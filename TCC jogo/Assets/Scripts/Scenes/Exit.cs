using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string Telainicial;
    public void Trigger()
    {
        SceneManager.LoadScene(Telainicial);
    }
}
