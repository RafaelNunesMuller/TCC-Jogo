using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public string LoadingFrase;
    public void Trigger()
    {
        SceneManager.LoadScene(LoadingFrase);
    }
}
