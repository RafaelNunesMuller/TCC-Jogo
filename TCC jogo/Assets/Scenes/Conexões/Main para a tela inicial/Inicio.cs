using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public string Frase_Inicial;
    public void Trigger()
    {
        SceneManager.LoadScene(Frase_Inicial);
    }
}

