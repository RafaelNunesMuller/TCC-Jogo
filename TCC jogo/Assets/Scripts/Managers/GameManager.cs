using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public playerStats playerStats;
    public string lastScene;
    public Vector3 lastPlayerPosition;

    private Player currentPlayer;

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
            StartCoroutine(RestaurarCenaCompleta());
        }
    }

    private IEnumerator RestaurarCenaCompleta()
    {
        yield return null;
        yield return new WaitForEndOfFrame();

        RestaurarReferenciasCena();

    }

    private void RestaurarReferenciasCena()
    {
        currentPlayer = FindAnyObjectByType<Player>();
        if (currentPlayer == null)
        {
            return;
        }

        if (lastPlayerPosition != Vector3.zero)
        {
            currentPlayer.transform.position = lastPlayerPosition;
        }

        if (playerStats == null)
        {
            var stats = currentPlayer.GetComponent<playerStats>();
            if (stats != null)
            {
                playerStats = stats;
            }
        }

        var camFollow = Camera.main?.GetComponent<CameraContoller>();
        if (camFollow != null)
        {
            camFollow.SetTarget(currentPlayer.transform);
            camFollow.transform.position = new Vector3(
                currentPlayer.transform.position.x,
                currentPlayer.transform.position.y,
                camFollow.transform.position.z
            );
        }

<<<<<<< HEAD
        // 🔹 5. Atualiza todas as UIs e menus
    foreach (var ui in FindObjectsByType<CombatUi>(FindObjectsSortMode.None))
        ui.playerStats = playerStats;
=======
        foreach (var ui in FindObjectsByType<CombatUi>(FindObjectsSortMode.None))
            ui.playerStats = playerStats;
>>>>>>> Ale

        foreach (var hp in FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None))
            hp.playerStats = playerStats;

        foreach (var status in FindObjectsByType<MenuStatus>(FindObjectsSortMode.None))
            status.playerStats = playerStats;

        foreach (var menu in FindObjectsByType<MenuController>(FindObjectsSortMode.None))
            menu.playerScript = currentPlayer;

    }
}
