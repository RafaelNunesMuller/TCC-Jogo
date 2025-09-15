using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public playerStats player;
    public List<EnemyStats> enemies = new List<EnemyStats>(); // preencher via EnemySpawner ou Inspector
    public float enemyAttackDelay = 0.7f; // tempo entre ataques inimigos

    // evita executar vários turnos de inimigo simultaneamente
    private bool isProcessingTurn = false;

    public void PlayerAttack(EnemyStats target, int damage)
    {
        if (isProcessingTurn) return;           // protege reentrância
        if (target == null || target.currentHP <= 0) return;

        int finalDamage = Mathf.Max(1, damage - target.defense);

        // usa o método do Enemy para lidar com morte/efeitos
        target.TakeDamage(finalDamage);

        Debug.Log($"🗡️ Player atacou {target.enemyName} e causou {finalDamage} de dano!");

        // começa o turno dos inimigos
        StartCoroutine(EnemiesTurn());
    }

    private IEnumerator EnemiesTurn()
    {
        if (isProcessingTurn) yield break;
        isProcessingTurn = true;

        // pequeno delay inicial (suaviza transições/efeitos)
        yield return new WaitForSeconds(0.2f);

        foreach (var inimigo in enemies)
        {
            if (inimigo == null) continue;
            if (inimigo.currentHP <= 0) continue; // pular inimigos mortos

            // escolhe um ataque do inimigo (se existir)
            Attack ataque = null;
            if (inimigo.attacks != null && inimigo.attacks.Length > 0)
                ataque = inimigo.ChooseAttack(); // ou escolhe aleatório dentro do EnemyStats

            int dano;
            string nomeAtk;
            if (ataque != null)
            {
                nomeAtk = ataque.nome;
                dano = Mathf.Max(1, inimigo.strength + ataque.power - player.DefenseTotal);
                // aplica dano ao player
                player.TakeDamage(dano);
            }
            else
            {
                nomeAtk = "Ataque";
                dano = Mathf.Max(1, inimigo.strength - player.DefenseTotal);
                // aplica dano ao player
                player.TakeDamage(dano);
            }

            
            if (player.currentHP < 0) player.currentHP = 0;

            Debug.Log($"{inimigo.enemyName} usou {nomeAtk} e causou {dano} de dano no player! (HP player {player.currentHP}/{player.maxHP})");

            // se o player morrer, encerra a coroutine
            if (player.currentHP <= 0)
            {
                Debug.Log("⚔️ Player derrotado!");
                isProcessingTurn = false;
                yield break;
            }

            // espera entre ataques para dar tempo a animações/popups
            yield return new WaitForSeconds(enemyAttackDelay);
        }

        // fim do turno inimigo — volta pro player
        Debug.Log("✨ Turno do Player novamente!");
        isProcessingTurn = false;

        yield break;
    }
}
