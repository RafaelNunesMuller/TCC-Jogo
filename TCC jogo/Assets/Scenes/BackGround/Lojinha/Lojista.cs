using UnityEngine;
using UnityEngine.SceneManagement;

public class Lojista : MonoBehaviour
{
    private bool Vendedor = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vendedor = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Vendedor = false;
    }

    void Update()
    {
        if (Vendedor && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Compras");
            Debug.Log("Item clicado");
        }
    }
}
