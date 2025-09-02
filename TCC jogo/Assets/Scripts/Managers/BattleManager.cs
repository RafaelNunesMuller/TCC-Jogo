using UnityEngine;
public class BattleManager : MonoBehaviour
{
    public playerStats player;
    public EnemyStats enemy;

    private bool playerTurn = true;

    // Faz o jogador atacar
    public void PlayerAttack(Attack atk)
    {
        if (!playerTurn)
        {
            Debug.Log("Não é seu turno!");
            return;
        }

        // Calcula dano
        int damage = CalculateDamage(atk);

        // Aplica dano ao inimigo
        enemy.TakeDamage(damage);

        Debug.Log($"Jogador usou {atk.nome} e causou {damage} de dano!");

        // Checa fim de batalha
        if(CheckBattleEnd()) return;

        // Passa o turno para o inimigo
        playerTurn = false;
        Invoke(nameof(EnemyAttack), 1f); // delay de 1s
    }

    public void PlayerAttackOnTarget(Attack atk, EnemyStats target)
{
    if (!playerTurn)
    {
        Debug.Log("Não é seu turno!");
        return;
    }

    int damage = Mathf.Max(0, atk.power + player.strength - target.defense);

    target.TakeDamage(damage);

    Debug.Log($"Jogador usou {atk.nome} em {target.enemyName} e causou {damage} de dano!");

    if(target.currentHP <= 0)
    {
        Debug.Log($"{target.enemyName} foi derrotado!");
    }

    playerTurn = false;
    Invoke(nameof(EnemyAttack), 1f);
}




    // Faz o inimigo atacar
    void EnemyAttack()
    {
        int damage = Mathf.Max(0, enemy.strength - player.defense);
        player.currentHP -= damage;

        Debug.Log($"{enemy.enemyName} atacou e causou {damage} de dano!");

        // Checa fim de batalha
        if(CheckBattleEnd()) return;

        // Volta o turno para o jogador
        playerTurn = true;
    }

    // Função que calcula dano
    int CalculateDamage(Attack atk)
    {
        return Mathf.Max(0, atk.power + player.strength - enemy.defense);
    }

    // Checa se a batalha terminou
    bool CheckBattleEnd()
    {
        if(player.currentHP <= 0)
        {
            Debug.Log("Jogador foi derrotado!");
            return true;
        }

        if(enemy.currentHP <= 0)
        {
            Debug.Log("Inimigo derrotado!");
            return true;
        }

        return false;
    }
}