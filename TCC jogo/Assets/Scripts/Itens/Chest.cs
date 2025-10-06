using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Configura��o do Ba�")]
    public Item itemDentro;                 // arraste o item que ser� dado ao jogador
    public bool foiAberto = false;          // controla se o ba� j� foi aberto
    public Sprite baulFechado;              // sprite do ba� fechado
    public Sprite baulAberto;               // sprite do ba� aberto
    private SpriteRenderer spriteRenderer;  // renderizador do ba�
    private bool playerPerto = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        // Garante que o ba� come�a fechado
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
            Debug.Log($"Voc� encontrou {itemDentro.nome}!");
        }
        else
        {
            Debug.LogWarning("Inventario.instance ou itemDentro est� nulo!");
        }
    }
}
