using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TargetMenu : MonoBehaviour
{
    public Button[] enemyButtons;
    public RectTransform cursor;
    public GameObject Menu;
    public CombatMenuController combatMenu;
    public BattleSystem battleSystem;

    private List<EnemyStats> inimigosAtivos = new List<EnemyStats>();
    private Attack ataqueAtual;
    private playerStats jogador;
    private int inimigoSelecionado = 0;
    public GameObject Cursor;


    public void ConfigurarInimigos(List<EnemyStats> inimigosAtivosRecebidos)
    {
        inimigosAtivos.Clear();
        if (inimigosAtivosRecebidos != null)
            inimigosAtivos.AddRange(inimigosAtivosRecebidos);

        for (int i = 0; i < enemyButtons.Length; i++)
        {
            if (i < inimigosAtivos.Count && inimigosAtivos[i] != null)
            {
                var inimigo = inimigosAtivos[i];
                enemyButtons[i].gameObject.SetActive(true);

                var refInimigo = enemyButtons[i].GetComponent<EnemyReference>();
                if (refInimigo == null)
                    refInimigo = enemyButtons[i].gameObject.AddComponent<EnemyReference>();
                refInimigo.enemy = inimigo;


                var tmp = enemyButtons[i].GetComponentInChildren<TMP_Text>();
                if (tmp != null)
                    tmp.text = inimigo.enemyName;

                int localIndex = i;

                inimigo.OnDeath += () =>
                {
                    enemyButtons[localIndex].gameObject.SetActive(false);
                    inimigosAtivos[localIndex] = null;

                    if (localIndex == inimigoSelecionado)
                        MoveToNext(1);
                };

            }
            else
            {
                enemyButtons[i].gameObject.SetActive(false);
            }
        }

        inimigoSelecionado = FirstActiveButtonIndex();
        AtualizarCursor();
    }

    public void AbrirSelecao(Attack ataque, playerStats jogadorRef)
    {
        ataqueAtual = ataque;
        jogador = jogadorRef;

        if (CountAliveInimigos() == 0)
        {
            return;
        }

        inimigoSelecionado = FirstActiveButtonIndex();
        AtualizarCursor();

        gameObject.SetActive(true);
        if (combatMenu != null)
            combatMenu.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveToNext(1);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveToNext(-1);

        if (Input.GetKeyDown(KeyCode.Z))
            ConfirmarAlvo();

        if (Input.GetKeyDown(KeyCode.X))
            VoltarParaMenu();
    }

    void MoveToNext(int dir)
    {
        if (enemyButtons.Length == 0) return;

        int len = enemyButtons.Length;
        int attempts = 0;

        do
        {
            inimigoSelecionado = (inimigoSelecionado + dir + len) % len;
            attempts++;
        }
        while (
            (inimigoSelecionado < inimigosAtivos.Count &&
             (inimigosAtivos[inimigoSelecionado] == null || !inimigosAtivos[inimigoSelecionado].IsAlive))
            && attempts < len);

        AtualizarCursor();
    }

    void AtualizarCursor()
    {
        if (enemyButtons.Length == 0) return;

        if (inimigoSelecionado < 0 || inimigoSelecionado >= enemyButtons.Length)
            inimigoSelecionado = FirstActiveButtonIndex();

        if (inimigoSelecionado < 0) return;

        cursor.SetParent(enemyButtons[inimigoSelecionado].transform, true);
        cursor.anchoredPosition = new Vector2(195, -15);
    }

    void ConfirmarAlvo()
    {
        if (inimigoSelecionado >= inimigosAtivos.Count)
            return;

        EnemyStats inimigo = inimigosAtivos[inimigoSelecionado];
        if (inimigo == null || !inimigo.IsAlive)
        {
            MoveToNext(1);
            return;
        }

        int dano = jogador.StrengthTotal + ataqueAtual.power;

        BattleSystem bs = battleSystem != null ? battleSystem : FindAnyObjectByType<BattleSystem>();
        if (bs != null)
            bs.PlayerAttack(inimigo, dano);
        else
            inimigo.TakeDamage(dano);
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        Menu.SetActive(true);
        if (combatMenu != null)
            combatMenu.enabled = true;
        Cursor.SetActive(true);
    }

    int FirstActiveButtonIndex()
    {
        for (int i = 0; i < enemyButtons.Length; i++)
            if (enemyButtons[i].gameObject.activeSelf)
                return i;
        return -1;
    }

    int CountAliveInimigos()
    {
        int c = 0;
        foreach (var e in inimigosAtivos)
            if (e != null && e.IsAlive) c++;
        return c;
    }
}
