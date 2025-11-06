using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximoAndar : MonoBehaviour
{
    public string andar = "";
    public float posicaox = 0;
    public float posicaoy = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(andar);

        }
    }
}
