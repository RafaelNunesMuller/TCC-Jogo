using UnityEngine;
using UnityEngine.SceneManagement; // precisa desse namespace

public class TrocarCena : MonoBehaviour
{
    

    void Start()
    {
        CarregarCena();
    }
    
    private void CarregarCena()
    {
        SceneManager.LoadScene("Casa");
    }
}

