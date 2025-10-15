using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    [Header("Referências")]
    public List<EnemyStats> inimigosAtivos;
    public playerStats player => GameManager.Instance.playerStats;

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
        yield return new WaitForSeconds(1.5f); // tempo antes de mostrar vitória

        // 🎯 pega referência ao player e ao XP ganho
        int xpGanho = CalcularXPInimigos(); // cria esse método abaixo
        var player = GameManager.Instance.playerStats;

        // Salva os status antigos (antes do level up)
        int oldLevel = player.level;
        int oldStr = player.strength;
        int oldDef = player.defense;
        int oldHP = player.maxHP;

        // Aplica XP e level up
        player.GainExperience(xpGanho);

        // Mostra tela de vitória
        VictoryUI victory = FindAnyObjectByType<VictoryUI>();
        if (victory != null)
        {
            victory.MostrarVitoria(player, xpGanho, oldLevel, oldStr, oldDef, oldHP);
            yield return new WaitUntil(() => victory.foiConfirmado);
        }

        // ✅ Garante que os stats salvos sejam mantidos
        GameManager.Instance.playerStats = player;

        // ✅ Volta pra última cena
        SceneManager.LoadScene(GameManager.Instance.lastScene);
    }

    int CalcularXPInimigos()
    {
        int totalXP = 0;
        foreach (var inimigo in inimigosAtivos)
        {
            if (inimigo != null)
                totalXP += inimigo.experienceReward;
        }
        return totalXP;
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
