using UnityEngine;
using System.Collections.Generic;
public class EnemyStats : MonoBehaviour
{
    public DamagePopupSpawner popupSpawner;

    [Header("Identificação")]
    public string enemyName;
    public int level;

    [Header("Atributos Base")]
    public int strength;
    public int defense;
    public int magic;
    public int magicDefense;

    [Header("HP")]
    public int maxHP;
    public int currentHP;

    [Header("Experiência ao morrer")]
    public int experienceReward;

    public event System.Action OnDeath;

    [Header("Ataques disponíveis do inimigo")]
    public Attack[] attacks; // arraste no Inspector

    public bool IsAlive => currentHP > 0;



    // Método para limpar handlers antigos
    public void ResetOnDeath()
    {
        OnDeath = null;
    }



    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"💥 {enemyName} recebeu {damage} de dano! (HP: {currentHP}/{maxHP})");

        if (!IsAlive)
        {
            Die();
           
        }
    }

    
    private void Die()
    {
        Debug.Log($"☠️ {enemyName} foi derrotado!");

        // Notifica o BattleSystem antes de destruir o inimigo
        BattleSystem bs = FindAnyObjectByType<BattleSystem>();
        if (bs != null)
        {
            if (!bs.inimigosAtivos.Contains(this))
                bs.inimigosAtivos.Add(this); // garante que está na lista

            // Marca HP como zero mas NÃO destrói ainda — BattleSystem cuidará disso
            currentHP = 0;
        }

        // Apenas desativa o inimigo visualmente (não destrói ainda)
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
