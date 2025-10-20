using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public playerStats playerStats;
    public string lastScene;
    public Vector3 lastPlayerPosition;
    public string pontoDeEntrada;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Battle")
        {
            StartCoroutine(RestaurarDepoisDeCarregar());
        }
    }

    public IEnumerator RestaurarDepoisDeCarregar()
    {
        // 🔹 Espera 1 frame para garantir que Player e UI existam
        yield return null;

        // 🔹 E mais 1 frame se precisar (em cenas mais pesadas)
        yield return new WaitForEndOfFrame();

        RestaurarReferenciasCena();
    }


    private void RestaurarReferenciasCena()
    {
        var player = FindAnyObjectByType<Player>();

        // 🔹 Só move o player se não estiver vindo de uma entrada específica
        if (player != null && playerStats != null && string.IsNullOrEmpty(pontoDeEntrada))
        {
            
            Debug.Log($"📍 Player restaurado para posição antiga: {lastPlayerPosition}");
        }
        else if (!string.IsNullOrEmpty(pontoDeEntrada))
        {
            Debug.Log($"🚪 Ignorando reposicionamento — vindo do ponto de entrada: {pontoDeEntrada}");
        }

        // 🔹 Reatribui câmera
        var camFollow = Camera.main?.GetComponent<CameraContoller>();
        if (camFollow != null && player != null)
        {
            camFollow.SetTarget(player.transform);
            camFollow.transform.position = new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                camFollow.transform.position.z
            );
        }

        // 🔹 Atualiza menus e UI
        foreach (var ui in FindObjectsByType<CombatUi>(FindObjectsSortMode.None))
            ui.playerStats = playerStats;

        foreach (var hp in FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None))
            hp.playerStats = playerStats;

        foreach (var status in FindObjectsByType<MenuStatus>(FindObjectsSortMode.None))
            status.playerStats = playerStats;

        Debug.Log("✅ Player, UI e Câmera restaurados.");
    }



}
