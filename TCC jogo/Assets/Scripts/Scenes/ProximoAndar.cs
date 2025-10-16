using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximoAndar : MonoBehaviour
{
    public string andar = "";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(andar);
        }
    }
}
