using UnityEngine;
using UnityEngine.SceneManagement;

public class Saída : MonoBehaviour
{
    public string Lojinha;
    public void Trigger()
    {
        SceneManager.LoadScene(Lojinha);
    }
}