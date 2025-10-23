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
        // Se não é batalha, restaura câmera, UI, etc.
        if (scene.name != "Battle")
        {
            StartCoroutine(RestaurarDepoisDeCarregar());
        }
    }

    private IEnumerator RestaurarDepoisDeCarregar()
    {
        yield return null; // espera 1 frame para garantir que tudo carregou
        yield return new WaitForEndOfFrame();

        RestaurarReferenciasCena();

        // Aguarda o SceneEntrance posicionar o player
        yield return new WaitForSeconds(0.05f);

        Debug.Log(" Cena restaurada com sucesso!");
    }

    private void RestaurarReferenciasCena()
    {
        var player = FindAnyObjectByType<Player>();
        if (player != null && playerStats != null)
        {
            // reposiciona o player na última posição salva
            player.transform.position = lastPlayerPosition;
        }

        // Restaura a câmera
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

        // Atualiza UIs e menus
        foreach (var ui in FindObjectsByType<CombatUi>(FindObjectsSortMode.None))
            ui.playerStats = playerStats;

        foreach (var hp in FindObjectsByType<PlayerHealth>(FindObjectsSortMode.None))
            hp.playerStats = playerStats;

        foreach (var status in FindObjectsByType<MenuStatus>(FindObjectsSortMode.None))
            status.playerStats = playerStats;

        Debug.Log(" Player e câmera restaurados após troca de cena.");
    }

}
