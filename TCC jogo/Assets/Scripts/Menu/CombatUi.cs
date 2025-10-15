using TMPro;
using UnityEngine;

public class CombatUi : MonoBehaviour
{
    public TMP_Text PlayerHP;
    private playerStats playerStats;

    void Start()
    {
        // 🔹 Pega o player atual do GameManager
        if (GameManager.Instance != null)
        {
            playerStats = GameManager.Instance.playerStats;
        }
        else
        {
            // fallback (caso o GameManager não esteja carregado ainda)
            playerStats = FindAnyObjectByType<playerStats>();
        }

        if (playerStats == null)
        {
            Debug.LogError("❌ Nenhum playerStats encontrado na cena!");
        }
    }

    void Update()
    {
        if (playerStats != null)
        {
            AtualizarStatus();
        }
    }

    void AtualizarStatus()
    {
        PlayerHP.text = $"HP: {playerStats.currentHP}/{playerStats.maxHP}";
    }
}
