using UnityEngine;
using UnityEngine.UI;

public class Placa4 : MonoBehaviour
{
    private bool Vendedor = false;
    public GameObject Fala;
    public Button Okay;

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
            Okay.onClick.AddListener(() => Fala.SetActive(false));
        }
    }
}
