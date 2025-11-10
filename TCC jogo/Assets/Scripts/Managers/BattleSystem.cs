using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
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
    yield return new WaitForSeconds(1.5f);

    int xpGanho = CalcularXPInimigos();
    var player = GameManager.Instance.playerStats;

    int oldLevel = player.level;
    int oldStr = player.strength;
    int oldDef = player.defense;
    int oldHP = player.maxHP;

    player.GainExperience(xpGanho);

    VictoryUI victory = FindAnyObjectByType<VictoryUI>();
    if (victory != null)
    {
        victory.MostrarVitoria(player, xpGanho, oldLevel, oldStr, oldDef, oldHP);
        yield return new WaitUntil(() => victory.foiConfirmado);
    }

    GameManager.Instance.playerStats = player;

    // Boss Final Apenas
    bool matouBossFinal = inimigosAtivos.Exists(e => e != null && e.bossFinal);

    if (matouBossFinal)
    {
        SceneManager.LoadScene("Tela de vitória");
    }
    else
    {
        SceneManager.LoadScene(GameManager.Instance.lastScene);
    }
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
    }

    public void PlayerAttack(EnemyStats target, int damage)
    {
        if (target == null || !target.IsAlive) return;

        int finalDamage = Mathf.Max(1, damage - target.defense);
        target.TakeDamage(finalDamage);

        StartCoroutine(EnemiesTurn());
    }

    public IEnumerator EnemiesTurn()
    {
        foreach (var inimigo in inimigosAtivos)
        {
            if (inimigo == null || !inimigo.IsAlive) continue;

            yield return new WaitForSeconds(1f);

            Attack ataque = inimigo.ChooseAttack();
            if (ataque != null)
            {
                int dano = Mathf.Max(1, inimigo.strength + ataque.power - player.DefenseTotal);
                player.TakeDamage(dano);
            }
        }

        yield return new WaitForSeconds(1.5f);
    }
}
