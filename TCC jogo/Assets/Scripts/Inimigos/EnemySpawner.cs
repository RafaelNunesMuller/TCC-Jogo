using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public Transform[] spawnPoints;

    public TargetMenu targetMenu;
    public BattleSystem battleSystem; 

    private List<EnemyStats> inimigosAtivos = new List<EnemyStats>();

    void Start()
    {
        SpawnarInimigosAleatorios();
    }

    void SpawnarInimigosAleatorios()
    {
        int qtdInimigos = Random.Range(1, spawnPoints.Length + 1);

        inimigosAtivos.Clear();

        for (int i = 0; i < qtdInimigos; i++)
        {
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject clone = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);

            EnemyStats stats = clone.GetComponent<EnemyStats>();
            inimigosAtivos.Add(stats);
        }

        if (targetMenu != null)
            targetMenu.ConfigurarInimigos(inimigosAtivos);

        if (battleSystem != null)
            battleSystem.SetEnemies(inimigosAtivos);
    }
}
