using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Configuração do Baú")]
    public Item itemDentro;
    public bool foiAberto = false;
    public Sprite baulFechado;
    public Sprite baulAberto;
    private SpriteRenderer spriteRenderer;

    private bool playerPerto = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && baulFechado != null)
            spriteRenderer.sprite = baulFechado;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerPerto = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerPerto = false;
    }

    void Update()
    {
        if (playerPerto && !foiAberto && Input.GetKeyDown(KeyCode.Z))
        {
            AbrirBau();
        }
    }

    void AbrirBau()
    {
        foiAberto = true;

        if (spriteRenderer != null && baulAberto != null)
            spriteRenderer.sprite = baulAberto;

        if (itemDentro != null)
        {
            Inventario.instance.Adicionar(itemDentro);
            string mensagem = $"Você encontrou {itemDentro.nome} x{itemDentro.quantidade}!";
            MessageUI.instance.ShowMessage(mensagem);
        }
        else
        {
            Debug.LogWarning("O baú não tem item dentro!");
        }
    }
}
