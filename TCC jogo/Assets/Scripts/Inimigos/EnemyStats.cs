using UnityEngine;
using System;
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
    public int speed;

    [Header("HP")]
    public int maxHP;
    public int currentHP;

    [Header("Experiência ao morrer")]
    public int experienceReward;

    public event System.Action OnDeath;

    // Método para limpar handlers antigos
    public void ResetOnDeath()
    {
        OnDeath = null;
    }

    

    public void TakeDamage(int dano)
    {
        currentHP -= dano;

        if (popupSpawner != null)
            popupSpawner.ShowDamage(dano);

        if (currentHP <= 0)
            Die();
    }



    void Die()
    {
        Debug.Log(enemyName + " foi derrotado!");

        // dispara evento só deste inimigo
        OnDeath?.Invoke();

        // destroi o inimigo da cena
        Destroy(gameObject);
    }


    void Start()
    {
        currentHP = maxHP;
    }
}
