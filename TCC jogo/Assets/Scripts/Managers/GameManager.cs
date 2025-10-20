using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public playerStats playerStats;
    public string lastScene;
    public Vector3 lastPlayerPosition;

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
        if (player != null && playerStats != null)
        {
            playerStats.transform.position = lastPlayerPosition;
        }

        var camFollow = Camera.main?.GetComponent<CameraContoller>();
        if (camFollow != null && player != null)
        {
            camFollow.SetTarget(player.transform); //  usa o método público
            camFollow.transform.position = new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                camFollow.transform.position.z
            );
        }

        foreach (var ui in FindObjectsByType<CombatUi>(FindObjectsSortMode.None))
            ui.playerStats = playerStats;

        foreach (var hp in FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None))
            hp.playerStats = playerStats;

        foreach (var status in FindObjectsByType<MenuStatus>(FindObjectsSortMode.None))
            status.playerStats = playerStats;

        Debug.Log("✅ Referências de Player, UI e Câmera restauradas.");
    }


}
