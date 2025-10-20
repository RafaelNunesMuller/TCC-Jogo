using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lojista : MonoBehaviour
{
    private bool Vendedor = false;
    public GameObject Fala;
    public Button Okay;
    public Button Exit;
    public GameObject Player;

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
            Fala.SetActive(true);
            Exit.onClick.AddListener(() => Fala.SetActive(false));
            Okay.onClick.AddListener(() => SceneManager.LoadScene("Compras"));
        }
    }
}
