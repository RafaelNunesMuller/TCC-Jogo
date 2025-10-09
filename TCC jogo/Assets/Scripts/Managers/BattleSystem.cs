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
        yield return new WaitForSeconds(1f); // pausa dramática 😎

        int totalXP = 0;
        foreach (var inimigo in inimigosAtivos)
        {
            if (inimigo != null)
                totalXP += inimigo.experienceReward;
        }

        // Salva status antigos para comparar depois
        int oldLevel = player.level;
        int oldStr = player.strength;
        int oldDef = player.defense;
        int oldHP = player.maxHP;

        player.GainExperience(totalXP);
        Debug.Log($"💫 Player ganhou {totalXP} XP!");

        // Chama tela de vitória
        var victoryUI = FindFirstObjectByType<VictoryUI>();
        if (victoryUI != null)
        {
            yield return victoryUI.MostrarVitoria(player, totalXP, oldLevel, oldStr, oldDef, oldHP);
        }

        // 🔹 Depois de apertar Z na tela de vitória, volta pra cena anterior
        if (!string.IsNullOrEmpty(GameManager.Instance.lastScene))
        {
            SceneManager.LoadScene(GameManager.Instance.lastScene);
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
