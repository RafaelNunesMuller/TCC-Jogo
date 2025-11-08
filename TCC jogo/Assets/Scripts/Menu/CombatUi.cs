using TMPro;
using UnityEngine;

public class CombatUi : MonoBehaviour
{
    public TMP_Text PlayerHP;
    public playerStats playerStats;

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
