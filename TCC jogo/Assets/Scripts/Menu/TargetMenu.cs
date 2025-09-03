using UnityEngine;
using UnityEngine.UI;

public class TargetMenu : MonoBehaviour
{
    public Button[] enemyButtons;   // Até 3 inimigos
    public RectTransform cursor;    // Cursor
    private int inimigoSelecionado = 0;

    private Attack ataqueAtual;
    private playerStats player;
    private EnemyStats[] inimigos;
    public GameObject Menu;

    public void ConfigurarInimigos(EnemyStats[] inimigosAtivos)
    {
        inimigos = inimigosAtivos;

        for (int i = 0; i < enemyButtons.Length; i++)
        {
            if (i < inimigos.Length && inimigos[i] != null)
            {
                enemyButtons[i].gameObject.SetActive(true);
                enemyButtons[i].GetComponentInChildren<TMPro.TMP_Text>();

            }
            else
            {
                enemyButtons[i].gameObject.SetActive(false);
            }
        }
    }


    public void AbrirSelecao(Attack ataque, playerStats jogador)
    {
        ataqueAtual = ataque;
        player = jogador;
        inimigoSelecionado = 0;

        if (inimigos != null && inimigos.Length > 0)
        {
            AtualizarCursor();
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Nenhum inimigo encontrado para atacar!");
        }
    }


    void Update()
    {
        if (!gameObject.activeSelf || inimigos == null || inimigos.Length == 0)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inimigoSelecionado = (inimigoSelecionado + 1) % inimigos.Length;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inimigoSelecionado--;
            if (inimigoSelecionado < 0) inimigoSelecionado = inimigos.Length - 1;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AtacarInimigo(inimigoSelecionado);
        }

        if (Input.GetKeyDown(KeyCode.X)) // botão voltar
        {
            VoltarParaMenu();
        }
    }


    void AtualizarCursor()
    {
        cursor.SetParent(enemyButtons[inimigoSelecionado].transform, true);
        cursor.anchoredPosition = new Vector2(195, -15);
    }

    void AtacarInimigo(int index)
    {
        if (inimigos[index] == null) return;

        int dano = Mathf.Max(1, (player.StrengthTotal + ataqueAtual.power) - inimigos[index].defense);
        inimigos[index].TakeDamage(dano);

        Debug.Log($"{player.name} usou {ataqueAtual.nome} em {inimigos[index]} e causou {dano} de dano! HP atual do inimigo {inimigos[index].currentHP}");

    }
    public void VoltarParaMenu()
    {
        Menu.SetActive(true);        // reabre o menu principal
    }
}
