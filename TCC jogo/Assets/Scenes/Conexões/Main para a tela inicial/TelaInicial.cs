using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{
    public string Telainicial;
    public void Trigger()
    {
        SceneManager.LoadScene(Telainicial);
    }


}
    