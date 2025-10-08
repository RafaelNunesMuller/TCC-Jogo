using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    [Header("Referências")]
    public playerStats player;
    public List<EnemyStats> inimigosAtivos;

    private bool batalhaEncerrada = false;

    void Update()
    {
        if (!batalhaEncerrada && TodosInimigosMortos())
        {
            batalhaEncerrada = true;
            StartCoroutine(FinalizarBatalha());
        }
    }

    bool TodosInimigosMortos()
    {
        return inimigosAtivos.TrueForAll(e => e == null || !e.IsAlive);
    }

    IEnumerator FinalizarBatalha()
    {
        Debug.Log("🎉 Todos os inimigos foram derrotados!");

        // Soma toda a XP dos inimigos
        int totalXP = 0;
        foreach (var inimigo in inimigosAtivos)
        {
            if (inimigo != null)
                totalXP += inimigo.experienceReward;
        }

        // Dá XP ao jogador
        player.GainExperience(totalXP);
        Debug.Log($"💫 Player ganhou {totalXP} XP!");

        // Mostra stats atualizados
        Debug.Log($"📈 Level: {player.level}, Força: {player.strength}, Defesa: {player.defense}, HP: {player.maxHP}");

        // Aguarda confirmação do jogador
        Debug.Log("Pressione Z para continuar...");
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

        // Retorna para a cena anterior
        if (!string.IsNullOrEmpty(GameManager.Instance.lastScene))
        {
            SceneManager.LoadScene(GameManager.Instance.lastScene);
        }
        else
        {
            Debug.LogError("⚠️ lastScene não definido!");
        }
    }

    public void SetEnemies(List<EnemyStats> novosInimigos)
    {
        inimigosAtivos = novosInimigos;
        Debug.Log($"⚔️ {inimigosAtivos.Count} inimigos entraram na batalha!");
    }

    public void PlayerAttack(EnemyStats target, int damage)
    {
        if (target == null || !target.IsAlive) return;

        int finalDamage = Mathf.Max(1, damage - target.defense);
        target.TakeDamage(finalDamage);

        Debug.Log($"🗡️ Player atacou {target.enemyName} e causou {finalDamage} de dano!");

        StartCoroutine(EnemiesTurn());
    }

    public IEnumerator EnemiesTurn()
    {
        Debug.Log("👹 Turno dos inimigos!");

        foreach (var inimigo in inimigosAtivos)
        {
            if (inimigo == null || !inimigo.IsAlive) continue;

            yield return new WaitForSeconds(1f);

            Attack ataque = inimigo.ChooseAttack();
            if (ataque != null)
            {
                int dano = Mathf.Max(1, inimigo.strength + ataque.power - player.DefenseTotal);
                player.TakeDamage(dano);

                Debug.Log($"{inimigo.enemyName} usou {ataque.nome} e causou {dano} de dano!");
            }
        }

        yield return new WaitForSeconds(1.5f);
        Debug.Log("✨ Turno do Player novamente!");
    }
}
