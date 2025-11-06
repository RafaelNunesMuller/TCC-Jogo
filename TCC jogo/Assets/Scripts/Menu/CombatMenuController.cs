using UnityEngine;
using UnityEngine.UI;

public class CombatMenuController : MonoBehaviour
{
    public Button[] opcoesMenu;
    public RectTransform cursor;
    private int opcaoSelecionada = 0;

    public GameObject Menu;
    public GameObject InventarioItem;
    public AttackMenu attackMenu;

    private MenuItem menuItemScript;
    public BattleItemMenu battleItemMenu;


    void Start()
    {
        AtualizarCursor();

        if (InventarioItem != null)
            menuItemScript = InventarioItem.GetComponent<MenuItem>();
    }

    void Update()
    {
        if (!Menu.activeSelf) return;

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ExecutarAcao(opcaoSelecionada);
        }
    }

    void AtualizarCursor()
    {
        cursor.SetParent(opcoesMenu[opcaoSelecionada].transform, true);
        cursor.anchoredPosition = new Vector2(195, -15);
    }

    void ExecutarAcao(int opcao)
    {
        switch (opcao)
        {
            case 0:
                attackMenu.gameObject.SetActive(true);
                Menu.SetActive(false);
                break;

            case 1: // item
                battleItemMenu.gameObject.SetActive(true);
                battleItemMenu.AbrirInventario();
                Menu.SetActive(false);
                break;


            case 2: // Fugir
                if (GameManager.Instance != null)
                {
                    string cenaVoltar = GameManager.Instance.lastScene;
                    UnityEngine.SceneManagement.SceneManager.LoadScene(cenaVoltar);
                }
                break;

        }
    }
}
