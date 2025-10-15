using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Dados do Player")]
    public playerStats playerStats;
    public Vector3 lastPlayerPosition;
    public string lastScene;

    [Header("Referências de UI e câmera")]
    public GameObject playerCameraPrefab;
    public GameObject menuPrefab;

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
        // Quando voltar pro mundo, restaura HUD e câmera
        if (scene.name == lastScene && scene.name != "Battle")
        {
            RestaurarCameraEMenus();
        }
    }

    public void RestaurarCameraEMenus()
    {
        // Recria câmera se não existir
        if (Camera.main == null && playerCameraPrefab != null)
        {
            Instantiate(playerCameraPrefab);
        }

        // Recria menus se não existir
        if (GameObject.FindAnyObjectByType<MenuController>() == null && menuPrefab != null)
        {
            Instantiate(menuPrefab);
        }
    }
}
