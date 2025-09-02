using UnityEngine;
using UnityEngine.UI;

public class AttackMenu : MonoBehaviour
{
    public GameObject buttonPrefab;      // prefab de botão
    public Transform buttonContainer;    // onde os botões vão aparecer
    public BattleManager battleManager;  // referência ao sistema de batalha

    public GameObject menuPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            VoltarParaMenu();
        }
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false); // fecha o Status
        menuPanel.SetActive(true);        // reabre o menu principal
    }

    public void ShowAttacks(playerStats player)
    {
        

        // Cria um botão para cada ataque
        foreach (Attack atk in player.ataques)
        {
            GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
            btnObj.GetComponentInChildren<Text>().text = atk.nome;

            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                battleManager.PlayerAttack(atk);
                gameObject.SetActive(false); // fecha o painel após escolher
            });
        }
    }
}
