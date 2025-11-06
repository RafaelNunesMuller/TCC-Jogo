using UnityEngine;
using UnityEngine.UI;

public class Placa2 : MonoBehaviour
{
    private bool placa2 = false;
    public GameObject Fala;
    public Button Okay;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            placa2 = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            placa2 = false;
    }

    void Update()
    {
        if (placa2 && Input.GetKeyDown(KeyCode.Z))
        {
            Fala.SetActive(true);
            Okay.onClick.AddListener(() => Fala.SetActive(false));
        }
    }
}
