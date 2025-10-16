using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public TargetMenu targetMenu;

   
    public DamagePopup damagePopupPrefab;

    public void PlayerAtacaInimigo(playerStats player, EnemyStats inimigo)
    {
        // 1. Calcula o dano
        int dano = Mathf.Max(0, player.strength - inimigo.defense);

        // 3. Spawna popup de dano
        DamagePopup popup = Instantiate(damagePopupPrefab, inimigo.transform.position, Quaternion.identity);
        popup.Setup(dano);

        // 4. Aplica o dano real no inimigo
        inimigo.TakeDamage(dano);
    }

    void Start()
    {
        // Acha todos os inimigos ativos na cena
        // pega todos inimigos ativos (lista) e converte para List<EnemyStats>
        EnemyStats[] inimigosArray = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);
        List<EnemyStats> inimigosList = new List<EnemyStats>(inimigosArray);

        // Configura o TargetMenu
        if (targetMenu != null)
        {
            targetMenu.ConfigurarInimigos(inimigosList);
        }
        else
        {
            Debug.LogWarning("TargetMenu n�o foi atribu�do no Inspector!");
        }
    }
}
