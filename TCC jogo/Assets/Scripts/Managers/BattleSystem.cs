using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("Referências")]
    public playerStats player;
    public List<EnemyStats> enemies; // lista dos inimigos da batalha

    // Chamado pelo Spawner quando a batalha começa
    public void SetEnemies(List<EnemyStats> novosInimigos)
    {
        enemies = novosInimigos;
        Debug.Log($"⚔️ {enemies.Count} inimigos entraram na batalha!");
    }

    // Player ataca um inimigo
    public void PlayerAttack(EnemyStats target, int damage)
    {
        if (target == null || !target.IsAlive) return;

        int finalDamage = Mathf.Max(1, damage - target.defense);
        target.TakeDamage(finalDamage);

        Debug.Log($"🗡️  atacou {target.enemyName} e causou {finalDamage} de dano!");

        // Se ainda houver inimigos vivos → inimigos atacam
        StartCoroutine(EnemiesTurn());
    }

    private IEnumerator EnemiesTurn()
    {

        Debug.Log("👹 Turno dos inimigos!");

        foreach (var inimigo in enemies)
        {
            if (inimigo == null || !inimigo.IsAlive) continue;

            yield return new WaitForSeconds(1f);

            Attack ataque = inimigo.ChooseAttack();
            if (ataque != null)
            {
                int dano = Mathf.Max(1, inimigo.strength + ataque.power - player.DefenseTotal);
                player.TakeDamage(dano);

                Debug.Log($"{inimigo.enemyName} usou {ataque.nome} e causou {dano} de dano em Player!");
            }
        }

        // Espera um pouquinho antes de devolver controle
        yield return new WaitForSeconds(0.5f);

        // Reabre o menu do Player
        if (player != null && player.GetComponent<CombatMenuController>() != null)
        {
            player.GetComponent<CombatMenuController>().enabled = true;
        }

        Debug.Log("✨ Turno do Player novamente!");
    }

}
