using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public playerStats playerStats;
    public HealthBar healthBar;

    private int lastHP;
    private int lastMaxHP;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            playerStats = GameManager.Instance.playerStats;
        }
        else
        {
            playerStats = FindAnyObjectByType<playerStats>();
        }

        if (playerStats == null)
        {
            enabled = false;
            return;
        }

        healthBar.UpdateHealthBar(playerStats.currentHP, playerStats.maxHP);
        lastHP = playerStats.currentHP;
        lastMaxHP = playerStats.maxHP;
    }

    void Update()
    {
        if (playerStats == null) return;

        if (playerStats.currentHP != lastHP || playerStats.maxHP != lastMaxHP)
        {
            healthBar.UpdateHealthBar(playerStats.currentHP, playerStats.maxHP);
            lastHP = playerStats.currentHP;
            lastMaxHP = playerStats.maxHP;
        }
    }
}
