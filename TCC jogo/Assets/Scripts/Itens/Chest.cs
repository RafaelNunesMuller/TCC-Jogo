using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Configuração do Baú")]
    public Item itemDentro;                 // arraste o item que será dado ao jogador
    public bool foiAberto = false;          // controla se o baú já foi aberto
    public Sprite baulFechado;              // sprite do baú fechado
    public Sprite baulAberto;               // sprite do baú aberto
    private SpriteRenderer spriteRenderer;  // renderizador do baú
    private bool playerPerto = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        // Garante que o baú começa fechado
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

        // Troca sprite pra "aberto"
        if (spriteRenderer != null && baulAberto != null)
            spriteRenderer.sprite = baulAberto;


        if (itemDentro != null)
        {
            Inventario.instance.Adicionar(itemDentro);
            Debug.Log($"Você encontrou {itemDentro.nome}!");
        }
        else
        {
            Debug.LogWarning("Inventario.instance ou itemDentro está nulo!");
        }
    }
}
