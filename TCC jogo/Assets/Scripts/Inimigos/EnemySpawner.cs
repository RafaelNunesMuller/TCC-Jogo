using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Lista de prefabs possíveis (até 10 inimigos)")]
    public GameObject[] enemyPrefabs;

    [Header("Locais de spawn (até 3 por batalha)")]
    public Transform[] spawnPoints;

    [Header("Referências")]
    public TargetMenu targetMenu;

    void Start()
    {
        SpawnarInimigosAleatorios();
    }

    void SpawnarInimigosAleatorios()
    {
        // Sorteia quantos inimigos vão aparecer (1 a 3)
        int qtdInimigos = Random.Range(1, spawnPoints.Length + 1);

        EnemyStats[] inimigosAtivos = new EnemyStats[qtdInimigos];

        for (int i = 0; i < qtdInimigos; i++)
        {
            // Sorteia um prefab da lista
            int indexPrefab = Random.Range(0, enemyPrefabs.Length);
            GameObject prefabEscolhido = enemyPrefabs[indexPrefab];

            // Instancia no ponto de spawn correspondente
            GameObject inimigoObj = Instantiate(prefabEscolhido, spawnPoints[i].position, Quaternion.identity, transform);

            // Pega o EnemyStats desse inimigo
            EnemyStats stats = inimigoObj.GetComponent<EnemyStats>();

            // Garante que o inimigo foi encontrado
            if (stats != null)
                inimigosAtivos[i] = stats;
            else
                Debug.LogError($"Prefab {prefabEscolhido.name} não tem EnemyStats!");
        }

        // Passa a lista para o TargetMenu
        if (targetMenu != null)
            targetMenu.ConfigurarInimigos(inimigosAtivos);
    }
}
