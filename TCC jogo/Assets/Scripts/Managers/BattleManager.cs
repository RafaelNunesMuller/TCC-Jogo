using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public TargetMenu targetMenu;

   
    public DamagePopup damagePopupPrefab;

    public void PlayerAtacaInimigo(playerStats player, EnemyStats inimigo)
    {
        int dano = Mathf.Max(0, player.strength - inimigo.defense);

        DamagePopup popup = Instantiate(damagePopupPrefab, inimigo.transform.position, Quaternion.identity);
        popup.Setup(dano);

        inimigo.TakeDamage(dano);
    }

    void Start()
    {
        EnemyStats[] inimigosArray = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);
        List<EnemyStats> inimigosList = new List<EnemyStats>(inimigosArray);

        if (targetMenu != null)
        {
            targetMenu.ConfigurarInimigos(inimigosList);
        }
    }
}
