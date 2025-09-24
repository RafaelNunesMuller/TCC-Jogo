using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TargetMenu : MonoBehaviour
{
    [Header("UI")]
    public Button[] enemyButtons;   // botões de seleção (UI) — tamanho fixo (ex.: 3)
    public RectTransform cursor;    // cursor visual
    public GameObject Menu; // painel do menu principal para reabrir
    public CombatMenuController combatMenu; // opcional: bloquear menu principal
    public BattleSystem battleSystem; // referência opcional ao BattleSystem (pode deixar vazio e será procurado)

    private List<EnemyStats> inimigosAtivos = new List<EnemyStats>();
    private Attack ataqueAtual;
    private playerStats jogador;
    private int inimigoSelecionado = 0;
    public GameObject Cursor;


    // Configura a lista de inimigos (chamado pelo spawner ou AttackMenu)
    public void ConfigurarInimigos(List<EnemyStats> inimigosAtivos)
    {
        if (inimigosAtivos.Count == 0)   // só na primeira vez
        {
            inimigosAtivos.Clear();
            inimigosAtivos.AddRange(inimigosAtivos);

            
    

            for (int i = 0; i < enemyButtons.Length; i++)
            {
                if (i < inimigosAtivos.Count && inimigosAtivos[i] != null)
                {
                    enemyButtons[i].gameObject.SetActive(true);

                    var refInimigo = enemyButtons[i].GetComponent<EnemyReference>();
                    if (refInimigo == null) refInimigo = enemyButtons[i].gameObject.AddComponent<EnemyReference>();
                    refInimigo.enemy = inimigosAtivos[i];

                    EnemyStats localEnemy = inimigosAtivos[i];
                    Button localButton = enemyButtons[i];
                    int localIndex = i;

                    localEnemy.OnDeath += () =>
                    {
                        localButton.gameObject.SetActive(false);
                        if (localIndex < inimigosAtivos.Count) inimigosAtivos[localIndex] = null;
                    };

                    // Se quiser texto, atualize aqui (não esqueça TMP)
                    var tmp = enemyButtons[i].GetComponentInChildren<TMP_Text>();
                    if (tmp != null) tmp.text = localEnemy.enemyName;
                }
                else
                {
                    enemyButtons[i].gameObject.SetActive(false);
                }
            }

            inimigoSelecionado = FirstActiveButtonIndex();
            AtualizarCursor();
        }
    }


    // Abre a seleção de alvo (chamada por AttackMenu)
    public void AbrirSelecao(Attack ataque, playerStats jogadorRef)
    {
        ataqueAtual = ataque;
        jogador = jogadorRef;

        if (CountAliveInimigos() == 0) return;

        inimigoSelecionado = FirstActiveButtonIndex();
        AtualizarCursor();

        gameObject.SetActive(true);
        if (combatMenu != null) combatMenu.enabled = false;
    }


    void Update()
    {
        Menu.SetActive(true);
        Cursor.SetActive(false);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveToNext(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveToNext(-1);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ConfirmarAlvo();
            
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            VoltarParaMenu();
        }
    }

    void MoveToNext(int dir)
    {
        int attempts = 0;
        int len = enemyButtons.Length;
        do
        {
            inimigoSelecionado = (inimigoSelecionado + dir + len) % len;
            attempts++;
        } while (!enemyButtons[inimigoSelecionado].gameObject.activeSelf && attempts <= len);
        AtualizarCursor();
    }

    void AtualizarCursor()
    {
        if (enemyButtons.Length == 0) return;
        if (!enemyButtons[inimigoSelecionado].gameObject.activeSelf)
        {
            inimigoSelecionado = FirstActiveButtonIndex();
            if (inimigoSelecionado < 0) return;
        }
        cursor.SetParent(enemyButtons[inimigoSelecionado].transform, true);
        cursor.anchoredPosition = new Vector2(195, -15);
    }

    void ConfirmarAlvo()
    {
        EnemyReference refInimigo = enemyButtons[inimigoSelecionado].GetComponent<EnemyReference>();
        if (refInimigo == null || refInimigo.enemy == null)
        {
            Debug.LogWarning("Alvo inválido.");
            return;
        }

        EnemyStats inimigo = refInimigo.enemy;

        // Calcula dano a partir do Attack ScriptableObject + força do player
        int dano = jogador.StrengthTotal + ataqueAtual.power;
  


        // chama o BattleSystem -> usa PlayerAttack(inimigo, dano)
        BattleSystem bs = battleSystem != null ? battleSystem : FindAnyObjectByType<BattleSystem>();
        if (bs != null)
        {
            bs.PlayerAttack(inimigo, dano);
        }
        else
        {
            // fallback: aplica direto no inimigo (se não tiver BattleSystem)
            inimigo.TakeDamage(dano);
        
        }

    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        Menu.SetActive(true);
        combatMenu.enabled = true;
    }

    int FirstActiveButtonIndex()
    {
        for (int i = 0; i < enemyButtons.Length; i++)
            if (enemyButtons[i].gameObject.activeSelf) return i;
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
