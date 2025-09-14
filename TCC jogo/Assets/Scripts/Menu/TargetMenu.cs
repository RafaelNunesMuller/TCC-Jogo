using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TargetMenu : MonoBehaviour
{
    public Button[] enemyButtons;   // Slots da UI (até 3 ou mais)
    public RectTransform cursor;
    private int inimigoSelecionado = 0;

    private Attack ataqueAtual;
    private playerStats player;

    private List<EnemyStats> inimigos = new List<EnemyStats>();

    public GameObject Menu;

    public GameObject Cursor;
    public CombatMenuController combatMenu;

    public void ConfigurarInimigos(EnemyStats[] inimigosAtivos)
    {
        inimigos.Clear();
        inimigos.AddRange(inimigosAtivos);

        for (int i = 0; i < enemyButtons.Length; i++)
        {
            if (i < inimigos.Count && inimigos[i] != null)
            {
                enemyButtons[i].gameObject.SetActive(true);

              

                // Linka botão com o inimigo spawnado
                var refInimigo = enemyButtons[i].GetComponent<EnemyReference>();
                if (refInimigo == null)
                    refInimigo = enemyButtons[i].gameObject.AddComponent<EnemyReference>();

                refInimigo.enemy = inimigos[i];

               

                int idx = i;
                inimigos[i].OnDeath += () =>
                {
                    enemyButtons[idx].gameObject.SetActive(false);
                    inimigos[idx] = null; // remove referência
                };
            }
            else
            {
                enemyButtons[i].gameObject.SetActive(false);
            }
        }

        inimigoSelecionado = 0;
        AtualizarCursor();
    }

    public void AbrirSelecao(Attack ataque, playerStats jogador)
    {
        ataqueAtual = ataque;
        player = jogador;

        if (inimigos.Count > 0)
        {
            inimigoSelecionado = 0;
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
        combatMenu.enabled = false;
        Menu.SetActive(true);
        Cursor.SetActive(false);

        if (!gameObject.activeSelf || inimigos.Count == 0)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoverCursor(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoverCursor(-1);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AtacarInimigo(inimigoSelecionado);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            VoltarParaMenu();
        }
    }

    void MoverCursor(int direcao)
    {
        int total = enemyButtons.Length;
        int tentativas = 0;

        do
        {
            inimigoSelecionado = (inimigoSelecionado + direcao + total) % total;
            tentativas++;
        }
        while ((!enemyButtons[inimigoSelecionado].gameObject.activeSelf) && tentativas <= total);

        AtualizarCursor();
    }

    void AtualizarCursor()
    {
        if (enemyButtons[inimigoSelecionado].gameObject.activeSelf)
        {
            cursor.SetParent(enemyButtons[inimigoSelecionado].transform, true);
            cursor.anchoredPosition = new Vector2(195, -15);
        }
    }

    void AtacarInimigo(int index)
    {
        if (index < 0 || index >= enemyButtons.Length) return;

        EnemyReference refInimigo = enemyButtons[index].GetComponent<EnemyReference>();
        if (refInimigo == null || refInimigo.enemy == null) return;

        EnemyStats inimigo = refInimigo.enemy;

        int dano = Mathf.Max(1, (player.StrengthTotal + ataqueAtual.power) - inimigo.defense);
        inimigo.TakeDamage(dano);

        Debug.Log($"{player.name} usou {ataqueAtual.nome} em {inimigo.enemyName} e causou {dano} de dano! HP atual: {inimigo.currentHP}");

        VoltarParaMenu();
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        Menu.SetActive(true);
        combatMenu.enabled = true;
        Cursor.SetActive(true);
    }
}
