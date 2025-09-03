using TMPro;
using UnityEngine;

public class CombatUi : MonoBehaviour
{

    public playerStats playerStats;
    
    public TMP_Text PlayerHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarStatus();
    }
    void AtualizarStatus()
    {
        PlayerHP.text =
            $"HP: {playerStats.currentHP}/{playerStats.maxHP}";
        
    }
}
