using UnityEngine;
using UnityEngine.UI;

public class AttackMenu : MonoBehaviour
{
    public Button[] attackButtons;     // Botões de ataques
    public RectTransform cursor;       // Cursor para navegação
    public TargetMenu targetMenu;
    public playerStats player;

    private int indiceSelecionado = 0;   // índice do botão
    private Attack ataqueAtual;          // ataque selecionado
    public GameObject Menu;

    void OnEnable()
    {
        indiceSelecionado = 0;
        AtualizarCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indiceSelecionado = (indiceSelecionado + 1) % attackButtons.Length;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indiceSelecionado--;
            if (indiceSelecionado < 0) indiceSelecionado = attackButtons.Length - 1;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelecionarAtaque();
        }

        if (Input.GetKeyDown(KeyCode.X)) // botão voltar
        {
            VoltarParaMenu();
        }
    }

    void AtualizarCursor()
    {
        cursor.SetParent(attackButtons[indiceSelecionado].transform, true);
        cursor.anchoredPosition = new Vector2(195, -15);
    }

    public void SelecionarAtaque()
    {
        // Pega o ataque do botão atual
        ataqueAtual = attackButtons[indiceSelecionado].GetComponent<AttackHolder>()?.attack;

        if (ataqueAtual == null)
        {
            Debug.LogWarning("Nenhum ataque configurado nesse botão!");
            return;
        }

        // Acha os inimigos
        EnemyStats[] inimigos = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None);
        targetMenu.ConfigurarInimigos(inimigos);

        // Abre o menu de seleção de alvo
        targetMenu.AbrirSelecao(ataqueAtual, player);
        gameObject.SetActive(false); // Fecha o menu de ataques
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false); // fecha o Status
        Menu.SetActive(true);        // reabre o menu principal
    }
}
