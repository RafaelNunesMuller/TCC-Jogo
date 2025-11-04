using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referências persistentes")]
    public playerStats playerStats;
    public string lastScene;
    public Vector3 lastPlayerPosition;

    private Player currentPlayer;

    void Awake()
    {
        // Garante que só exista 1 GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Escuta mudanças de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ignora se for batalha (para não mostrar menus etc.)
        if (scene.name != "Battle")
        {
            StartCoroutine(RestaurarCenaCompleta());
        }
    }

    private IEnumerator RestaurarCenaCompleta()
    {
        // Espera o Unity terminar de carregar a cena e os objetos
        yield return null;
        yield return new WaitForEndOfFrame();

        RestaurarReferenciasCena();

        Debug.Log("✅ Cena e referências restauradas com sucesso!");
    }

    private void RestaurarReferenciasCena()
    {
        // 🔹 1. Localiza o Player na cena
        currentPlayer = FindAnyObjectByType<Player>();
        if (currentPlayer == null)
        {
            Debug.LogWarning("⚠ Nenhum Player encontrado na cena!");
            return;
        }

        // 🔹 2. Reposiciona o player corretamente
        if (lastPlayerPosition != Vector3.zero)
        {
            currentPlayer.transform.position = lastPlayerPosition;
            Debug.Log($"📍 Player reposicionado em {lastPlayerPosition}");
        }

        // 🔹 3. Reatribui o playerStats ao GameManager (se precisar)
        if (playerStats == null)
        {
            var stats = currentPlayer.GetComponent<playerStats>();
            if (stats != null)
            {
                playerStats = stats;
                Debug.Log("♻ playerStats reassociado ao GameManager.");
            }
        }

        // 🔹 4. Restaura a câmera principal
        var camFollow = Camera.main?.GetComponent<CameraContoller>();
        if (camFollow != null)
        {
            camFollow.SetTarget(currentPlayer.transform);
            camFollow.transform.position = new Vector3(
                currentPlayer.transform.position.x,
                currentPlayer.transform.position.y,
                camFollow.transform.position.z
            );
            Debug.Log("🎥 Câmera reconectada ao Player.");
        }

        // 🔹 5. Atualiza todas as UIs e menus
    foreach (var ui in FindObjectsByType<CombatUi>(FindObjectsSortMode.None))
        ui.playerStats = playerStats;

    foreach (var hp in FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None))
        hp.playerStats = playerStats;

    foreach (var status in FindObjectsByType<MenuStatus>(FindObjectsSortMode.None))
        status.playerStats = playerStats;

    foreach (var menu in FindObjectsByType<MenuController>(FindObjectsSortMode.None))
        menu.playerScript = currentPlayer;

    // 🔹 Novo: restaura o MenuEquip também
    foreach (var equip in FindObjectsByType<MenuEquip>(FindObjectsSortMode.None))
    {
        equip.playerStats = playerStats;

        // garante que o prefab está configurado (se tiver sido perdido)
        if (equip.equipItemPrefab == null)
        {
            equip.equipItemPrefab = Resources.Load<GameObject>("Prefabs/EquipItemPrefab");
            Debug.Log("🔧 EquipItemPrefab restaurado via Resources.");
        }
    }

Debug.Log("🧩 Referências de UI, menus e equipamentos restauradas.");


        Debug.Log("🧩 Referências de UI e menus restauradas.");
    }
}
