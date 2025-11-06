using UnityEngine;
using UnityEngine.SceneManagement;

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

