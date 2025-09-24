using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMenu : MonoBehaviour
{
    public Button[] attackButtons;     // Lista de ataques
    public RectTransform cursor;       // Cursor para navegação
    public TargetMenu targetMenu;
    public playerStats player;
    public CombatMenuController combatMenu;
    public GameObject menu;
    public GameObject targetMenuUI;
    public GameObject CursorAttack;


    private int ataqueSelecionado = 0;


    void Start()
    {
        CursorAttack.SetActive(true);
    }

    void OnEnable()
    {
        ataqueSelecionado = 0;
        AtualizarCursor();
        combatMenu.enabled = false; // 🔹 desativa menu principal enquanto escolhe ataque
        
    }

    void OnDisable()
    {
        combatMenu.enabled = true; // 🔹 reativa menu principal ao fechar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ataqueSelecionado = (ataqueSelecionado + 1) % attackButtons.Length;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ataqueSelecionado--;
            if (ataqueSelecionado < 0) ataqueSelecionado = attackButtons.Length - 1;
            AtualizarCursor();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SelecionarAtaque();

        }

        if (Input.GetKeyDown(KeyCode.X)) // botão de voltar
        {
            VoltarParaMenu();
        }
    }

    void AtualizarCursor()
    {
        cursor.SetParent(attackButtons[ataqueSelecionado].transform, true);
        cursor.anchoredPosition = new Vector2(195, -15);
    }

    public void SelecionarAtaque()
    {
        if (targetMenuUI != null) targetMenuUI.SetActive(true);

        Attack ataque = attackButtons[ataqueSelecionado]
                           .GetComponent<AttackReference>().attack;
        if (ataque == null) return;

        BattleSystem bs = FindFirstObjectByType<BattleSystem>();
        if (bs != null && bs.inimigosAtivos != null)
            targetMenu.ConfigurarInimigos(bs.inimigosAtivos);  // ✅ clones corretos

        targetMenu.AbrirSelecao(ataque, player);
        gameObject.SetActive(false);
    }



    public void VoltarParaMenu()
    {
        gameObject.SetActive(false); // fecha o Status
        menu.SetActive(true);        // reabre o menu principal
    }
}
