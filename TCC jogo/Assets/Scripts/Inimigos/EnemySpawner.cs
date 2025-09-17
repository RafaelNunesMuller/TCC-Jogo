using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs de inimigos possíveis")]
    public GameObject[] enemyPrefabs;

    [Header("Locais de spawn")]
    public Transform[] spawnPoints;

    [Header("Referências")]
    public TargetMenu targetMenu;
    public BattleSystem battleSystem; // <-- referência ao BattleSystem

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

        // 🔹 Passa inimigos reais para o TargetMenu
        if (targetMenu != null)
            targetMenu.ConfigurarInimigos(inimigosAtivos);

        // 🔹 Passa inimigos reais para o BattleSystem
        if (battleSystem != null)
            battleSystem.SetEnemies(inimigosAtivos);
    }
}
