using UnityEngine;
using System.Collections.Generic;

public class EnemyStats : MonoBehaviour
{
    public DamagePopupSpawner popupSpawner;
    public bool bossFinal;

    public string enemyName;
    public int level;

    public int strength;
    public int defense;
    public int magic;
    public int magicDefense;

    public int maxHP;
    public int currentHP;

    public int experienceReward;
    public int coinReward = 50; //  Moedas que o inimigo dá ao morrer

    public event System.Action OnDeath;

    public Attack[] attacks;

    public bool IsAlive => currentHP > 0;

    public void ResetOnDeath()
    {
        OnDeath = null;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        if (!IsAlive)
        {
            Die();
        }
    }

    private void Die()
    {
        BattleSystem bs = FindAnyObjectByType<BattleSystem>();
        if (bs != null)
        {
            if (!bs.inimigosAtivos.Contains(this))
                bs.inimigosAtivos.Add(this);

            currentHP = 0;
        }

        //  Adiciona moedas ao player quando o inimigo morre
        if (CoinManager.instance != null)
        {
            CoinManager.instance.AddCoins(coinReward);
            Debug.Log($"{enemyName} derrotado! +{coinReward} moedas.");
        }

        gameObject.SetActive(false);
    }

    public Attack ChooseAttack()
    {
        if (attacks == null || attacks.Length == 0) return null;
        return attacks[Random.Range(0, attacks.Length)];
    }

    void Start()
    {
        currentHP = maxHP;
    }
}
