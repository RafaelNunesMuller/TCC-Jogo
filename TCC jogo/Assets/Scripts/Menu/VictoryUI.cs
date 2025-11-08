using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryUI : MonoBehaviour
{
    public GameObject victoryPanel;
    public TMP_Text textoMensagem;
    public TMP_Text textoStats;

    public bool foiConfirmado = false;

    public void MostrarVitoria(playerStats player, int xpTotal, int oldLevel, int oldStr, int oldDef, int oldHP)
    {
        victoryPanel.SetActive(true);
        foiConfirmado = false;

        string msg = $"🏆 Vitória!\n\n" +
                     $"Ganhou {xpTotal} XP!\n\n" +
                     $"Level: {oldLevel} → {player.level}\n" +
                     $"Força: {oldStr} → {player.strength}\n" +
                     $"Defesa: {oldDef} → {player.defense}\n" +
                     $"HP Máx: {oldHP} → {player.maxHP}";

        textoMensagem.text = msg;
    }

    void Update()
    {
        if (victoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            foiConfirmado = true;
            victoryPanel.SetActive(false);
        }
    }
}
