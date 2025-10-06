using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class GameManager : MonoBehaviour
{
     public static GameManager Instance;

     public string lastScene;
     public Vector3 lastPlayerPosition;
  


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
