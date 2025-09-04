using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TargetMenu : MonoBehaviour
{
    public Button[] enemyButtons;   // Até 3 inimigos
    public RectTransform cursor;    // Cursor
    private int inimigoSelecionado = 0;

    private Attack ataqueAtual;
    private playerStats player;
    private EnemyStats[] inimigos;
    public GameObject Menu;
    public CombatMenuController combatMenu; // ref. ao menu principal

    public void ConfigurarInimigos(EnemyStats[] inimigosAtivos)
    {
        inimigos = inimigosAtivos;

        for (int i = 0; i < enemyButtons.Length; i++)
        {
            if (i < inimigos.Length && inimigos[i] != null)
            {
                enemyButtons[i].gameObject.SetActive(true);

                var refInimigo = enemyButtons[i].GetComponent<EnemyReference>();
                if (refInimigo == null)
                    refInimigo = enemyButtons[i].gameObject.AddComponent<EnemyReference>();

                refInimigo.enemy = inimigos[i];

                // 🔹 Limpa handlers antigos corretamente
                inimigos[i].ResetOnDeath();

                // 🔹 Captura a referência local
                Button botao = enemyButtons[i];
                EnemyStats inimigo = inimigos[i];

                inimigo.OnDeath += () =>
                {
                    botao.gameObject.SetActive(false);
                };
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
            combatMenu.enabled = false; // ?? bloqueia menu principal
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

        if (Input.GetKeyDown(KeyCode.X)) // botão de voltar
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
        EnemyReference refInimigo = enemyButtons[index].GetComponent<EnemyReference>();
        if (refInimigo == null || refInimigo.enemy == null) return;

        EnemyStats inimigo = refInimigo.enemy;

        int dano = Mathf.Max(1, (player.StrengthTotal + ataqueAtual.power) - inimigo.defense);
        inimigo.TakeDamage(dano);

        Debug.Log($"{player.name} usou {ataqueAtual.nome} em {inimigo.enemyName} e causou {dano} de dano! HP atual: {inimigo.currentHP}");

        VoltarParaMenu(); // volta pro menu depois do ataque
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        Menu.SetActive(true);
        combatMenu.enabled = true; // ?? reativa menu principal
    }
}
