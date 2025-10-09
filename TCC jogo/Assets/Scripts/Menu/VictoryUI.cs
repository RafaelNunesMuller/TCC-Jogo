using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryUI : MonoBehaviour
{
    public GameObject victoryPanel;
    public TMP_Text messageText;

    private bool readyToContinue = false;

    public IEnumerator MostrarVitoria(playerStats player, int xpTotal, int oldLevel, int oldStr, int oldDef, int oldHP)
    {
        victoryPanel.SetActive(true);
        readyToContinue = false;

        // Mensagem formatada
        messageText.text =
            $"Vitória!\n\n" +
            $"Ganhou {xpTotal} XP!\n\n" +
            $" Nível: {oldLevel}->" +
            $" Nível: {player.level}\n"+
            $" Força: {oldStr}->" +
            $" Força: {player.strength}\n" +
            $" Defesa: {oldDef}->" +
            $" Defesa: {player.defense}\n" +
            $" HP Máx: {oldHP}->" +            
            $" HP Máx: {player.maxHP}\n" +
            $"Pressione [Z] para continuar...";

        // Espera o jogador confirmar
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

        victoryPanel.SetActive(false);
    }

    void Update()
    {
        if (victoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            readyToContinue = true;
            Fechar();
        }
    }

    void Fechar()
    {
        victoryPanel.SetActive(false);

        // Volta para a cena anterior
        if (!string.IsNullOrEmpty(GameManager.Instance.lastScene))
            SceneManager.LoadScene(GameManager.Instance.lastScene);
    }
}
