using TMPro;
using UnityEngine;

public class MenuStatus : MonoBehaviour
{
    public playerStats playerStats;
    public RectTransform cursor;
    public TMP_Text statusText;

    [Header("Refer�ncias")]
    public GameObject menuPanel; // arrasta o MenuPanel no inspector

    void Update()
    {
        AtualizarStatus();
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            VoltarParaMenu();
        }
    }

    void AtualizarStatus()
    {
        statusText.text =
            $"LV: {playerStats.level}\n" +
            $"EXPERIENCE: {playerStats.experience}\n" +
            $"HP: {playerStats.maxHP}\n" +
            $"STRENGTH: {playerStats.strength}\n" +
            $"DEFENSE: {playerStats.defense}\n" +
            $"MAGIC: {playerStats.magic}\n" +
            $"MAGIC DEFENSE: {playerStats.magicDefense}\n";

    }

    // --- Fun��o para o bot�o ---
    public void VoltarParaMenu()
    {
        gameObject.SetActive(false); // fecha o Status
        menuPanel.SetActive(true);        // reabre o menu principal
    }
}
