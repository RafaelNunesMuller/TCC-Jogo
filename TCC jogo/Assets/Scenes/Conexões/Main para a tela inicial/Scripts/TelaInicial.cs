using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{
    public string Loading;
    public void Trigger()
    {
        SceneManager.LoadScene(Loading);
    }


}
    