using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
    public string Casa;
    public void Trigger()
    {
        SceneManager.LoadScene(Casa);
    }
}

