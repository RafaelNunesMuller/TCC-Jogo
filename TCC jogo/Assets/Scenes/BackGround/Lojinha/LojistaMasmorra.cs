using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LojistaMasmorra : MonoBehaviour
{
    public bool Vendedor = false;
    public GameObject Fala;
    public Button Okay;
    public Button Exit;

    void Start()
    {
        if (Okay != null)
            Okay.onClick.RemoveAllListeners();

        if (Exit != null)
            Exit.onClick.RemoveAllListeners();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Vendedor = true;
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

            Exit.onClick.RemoveAllListeners();
            Exit.onClick.AddListener(() =>
            {
                Debug.Log("🧾 Fechar diálogo da loja da masmorra");
                Fala.SetActive(false);
            });

            Okay.onClick.RemoveAllListeners();
            Okay.onClick.AddListener(() =>
            {
                Debug.Log("🛒 Carregando cena: Compras (Masmorra)");
                SceneManager.LoadScene("Compras (Masmorra)");
            });
        }
    }
}
