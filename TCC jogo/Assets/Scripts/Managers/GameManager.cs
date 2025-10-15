using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string lastScene;
    public Vector3 playerPosition;
    internal Vector3 lastPlayerPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Instance != null)
        {
            var player = FindFirstObjectByType<playerStats>();
            if (player != null)
                player.transform.position = GameManager.Instance.playerPosition;
        }
    }

}
