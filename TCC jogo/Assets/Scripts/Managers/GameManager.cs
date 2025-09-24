using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public string lastScene;
    [HideInInspector] public Vector3 lastPlayerPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 🔹 mantém este objeto entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
