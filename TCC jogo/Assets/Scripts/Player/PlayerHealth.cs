using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public playerStats playerStats; 
    public HealthBar healthBar;     

    void Start()
    {
        
    }

    void Update()
    {
            // garante que a barra sempre reflete os valores atuais
            healthBar.UpdateHealthBar(playerStats.currentHP, playerStats.maxHP);
    }
}
