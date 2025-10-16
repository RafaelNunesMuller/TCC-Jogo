using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private playerStats playerStats;
    public HealthBar healthBar;

    private int lastHP;
    private int lastMaxHP;

    void Start()
    {
        // 🔹 Busca o player persistente
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
            Debug.LogError("❌ Nenhum playerStats encontrado na cena!");
            enabled = false;
            return;
        }

        // Inicializa a barra com o valor atual
        healthBar.UpdateHealthBar(playerStats.currentHP, playerStats.maxHP);
        lastHP = playerStats.currentHP;
        lastMaxHP = playerStats.maxHP;
    }

    void Update()
    {
        if (playerStats == null) return;

        // 🔹 Só atualiza a barra quando algo muda
        if (playerStats.currentHP != lastHP || playerStats.maxHP != lastMaxHP)
        {
            healthBar.UpdateHealthBar(playerStats.currentHP, playerStats.maxHP);
            lastHP = playerStats.currentHP;
            lastMaxHP = playerStats.maxHP;
        }
    }
}
