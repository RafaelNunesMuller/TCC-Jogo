using TMPro;
using UnityEngine;

public class MenuStatus : MonoBehaviour
{
    [Header("Referências")]
    public GameObject menuPanel;
    public RectTransform cursor;
    public TMP_Text statusText;

    [Header("Dados")]
    public playerStats playerStats;

    void OnEnable()
    {
        //  Repega o playerStats salvo no GameManager
        if (GameManager.Instance != null && GameManager.Instance.playerStats != null)
        {
            playerStats = GameManager.Instance.playerStats;
            Debug.Log(" MenuStatus reassociou o playerStats do GameManager.");
        }
        else if (playerStats == null)
        {
            playerStats = FindAnyObjectByType<playerStats>();
            Debug.LogWarning(" Nenhum playerStats no GameManager, usando o da cena.");
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
