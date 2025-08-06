using UnityEngine;
using UnityEngine.UI;

public class CombatMenuController : MonoBehaviour
{
    public Text[] opcoesMenu;           // As opções do menu (Text ou TMP_Text)
    public RectTransform cursor;        // O cursor que será movido
    private int opcaoSelecionada = 0;

    void Start()
    {
        AtualizarCursor();
        AtualizarSelecaoVisual();
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
    void AtualizarSelecaoVisual()
    {
        for (int i = 0; i < opcoesMenu.Length; i++)
        {
            opcoesMenu[i].color = Color.white;
        }
    }

    void AtualizarCursor()
    {
        // Reposiciona o cursor como filho do botão atual (alinha perfeitamente)
        cursor.SetParent(opcoesMenu[opcaoSelecionada].transform, false);

        // Posiciona um pouco à esquerda do texto
        cursor.anchoredPosition = new Vector2(200, 0); // distância da esquerda
    }


    void ExecutarAcao(int opcao)
    {
        switch (opcao)
        {
            case 0:
                Debug.Log("ATACAR!");
                break;
            case 1:
                Debug.Log("ITEM!");
                break;
            case 2:
                Debug.Log("FUGIR!");
                break;
        }
    }
}
