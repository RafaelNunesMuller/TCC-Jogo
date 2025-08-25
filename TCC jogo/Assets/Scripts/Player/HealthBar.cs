using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFill; 

    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (maxHP <= 0)
        {
            healthFill.fillAmount = 0;
            return;
        }

        
        healthFill.fillAmount = (float)currentHP / maxHP;
    }
}
