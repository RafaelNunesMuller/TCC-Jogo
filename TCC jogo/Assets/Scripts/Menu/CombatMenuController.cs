using UnityEngine;
using UnityEngine.UI;

public class CombatMenuController : MonoBehaviour
{
    public Button[] opcoesMenu;           // As op��es do menu (Text ou TMP_Text)
    public RectTransform cursor;        // O cursor que ser� movido
    private int opcaoSelecionada = 0;
    public static Inventario instance;
    public MenuItem menuItem;
    public GameObject Menu;
    public GameObject InventarioItem;

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
