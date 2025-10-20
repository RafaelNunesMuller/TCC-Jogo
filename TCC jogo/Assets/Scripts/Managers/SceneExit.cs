using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public string cenaDestino; // nome da cena para onde vai
    public string entradaDestinoID; // ID do ponto onde o player deve aparecer

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.pontoDeEntrada = entradaDestinoID;
            SceneManager.LoadScene(cenaDestino);
        }
    }
}
