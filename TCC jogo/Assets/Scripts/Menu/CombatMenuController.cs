using UnityEngine;
using UnityEngine.UI;

public class CombatMenuController : MonoBehaviour
{
     public Button[] opcoesMenu;      // Opções do menu (Atacar, Item, Fugir)
    public RectTransform cursor;     // O cursor do menu
    private int opcaoSelecionada = 0;

    [Header("Menus")]
    public GameObject Menu;          // Menu principal
    public GameObject InventarioItem;// Menu de itens

    [Header("Referências de Combate")]
    public AttackMenu attackMenu;    // Script do menu de ataques
    public playerStats player;       // Referência ao Player

    void Start()
    {
        AtualizarCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            opcaoSelecionada = (opcaoSelecionada + 1) % opcoesMenu.Length;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            opcaoSelecionada--;
            if (opcaoSelecionada < 0)
                opcaoSelecionada = opcoesMenu.Length - 1;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            ExecutarAcao(opcaoSelecionada);
        }
    }
    

    void AtualizarCursor()
    {
        // Reposiciona o cursor como filho do bot�o atual (alinha perfeitamente)
        cursor.SetParent(opcoesMenu[opcaoSelecionada].transform, true);

        // Posiciona um pouco � esquerda do texto
        cursor.anchoredPosition = new Vector2(195, -15); // dist�ncia da esquerda
    }


    void ExecutarAcao(int opcao)
    {
        switch (opcao)
        {
            case 0:
                attackMenu.gameObject.SetActive(true); // abre o painel de ataques
                attackMenu.ShowAttacks(player);        // gera os botões
                Menu.SetActive(false);                 // fecha o menu principal
                Debug.Log("ATACAR!");
                break;
            case 1:
                InventarioItem.SetActive(true);
                Menu.SetActive(false); // Fecha o menu principal
                Debug.Log("Abrir ITEM");
                break;
            case 2:
                Debug.Log("FUGIR!");
                break;
        }
    }
}
