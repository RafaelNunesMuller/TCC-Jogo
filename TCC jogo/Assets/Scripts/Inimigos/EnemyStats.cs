using UnityEngine;

public class EnemyStats : MonoBehaviour
{
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

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log(enemyName + " foi derrotado!");
        Destroy(gameObject);
    }

    void Start()
    {
        currentHP = maxHP;
    }
}
