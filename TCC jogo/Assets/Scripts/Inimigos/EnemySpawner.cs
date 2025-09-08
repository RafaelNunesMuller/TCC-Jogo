using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs dos inimigos (com SpriteRenderer + EnemyStats)")]
    public EnemyStats[] enemyPrefabs;

    [Header("Pontos de spawn")]
    public Transform[] spawnPoints;

    [Header("Referência ao TargetMenu")]
    public TargetMenu targetMenu;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        int n = Mathf.Min(enemyPrefabs.Length, spawnPoints.Length);
        EnemyStats[] ativos = new EnemyStats[n];

        for (int i = 0; i < n; i++)
        {
            if (enemyPrefabs[i] == null || spawnPoints[i] == null) continue;

            EnemyStats inst = Instantiate(enemyPrefabs[i], spawnPoints[i].position, Quaternion.identity);

            // garantir que aparece
            SpriteRenderer sr = inst.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = "Characters";
                sr.sortingOrder = 5;
            }

            ativos[i] = inst;
        }

        // passa a lista de inimigos vivos para o TargetMenu
        if (targetMenu != null)
            targetMenu.ConfigurarInimigos(ativos);
    }
}
