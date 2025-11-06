using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{
    public string Loading;
    public void Trigger()
    {
        SceneManager.LoadScene(Loading);
    }


}

