using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public TargetMenu targetMenu;

    void Start()
    {
        // Acha todos os inimigos ativos na cena
        EnemyStats[] inimigos = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);

        // Configura o TargetMenu
        if (targetMenu != null)
        {
            targetMenu.ConfigurarInimigos(inimigos);
        }
        else
        {
            Debug.LogWarning("TargetMenu não foi atribuído no Inspector!");
        }
    }
}
