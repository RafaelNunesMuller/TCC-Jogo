using TMPro;
using UnityEngine;

public class MenuStatus : MonoBehaviour
{
    public GameObject menuPanel;
    public RectTransform cursor;
    public TMP_Text statusText;

    public playerStats playerStats;

    void OnEnable()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerStats != null)
        {
            playerStats = GameManager.Instance.playerStats;
        }
        else if (playerStats == null)
        {
            playerStats = FindAnyObjectByType<playerStats>();
        }

        AtualizarStatus();
    }

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
        if (playerStats == null)
        {
            statusText.text = " PlayerStats não encontrado!";
            return;
        }

        statusText.text =
            $"LV: {playerStats.level}\n" +
            $"EXPERIENCE: {playerStats.experience}\n" +
            $"HP: {playerStats.currentHP}/{playerStats.maxHP}\n" +
            $"STRENGTH: {playerStats.strength}\n" +
            $"DEFENSE: {playerStats.defense}\n" +
            $"MAGIC: {playerStats.magic}\n" +
            $"MAGIC DEFENSE: {playerStats.magicDefense}\n";
    }

    public void VoltarParaMenu()
    {
        gameObject.SetActive(false);
        menuPanel.SetActive(true);
    }
}
