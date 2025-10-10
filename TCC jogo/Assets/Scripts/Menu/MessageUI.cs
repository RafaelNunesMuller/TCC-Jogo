using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public static MessageUI instance;
    public GameObject panel;
    public TextMeshProUGUI messageText;

    private bool aguardandoEntrada = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        

        panel.SetActive(false);
    }

    public void ShowMessage(string texto)
    {
        messageText.text = texto;
        panel.SetActive(true);
        StartCoroutine(EsperarConfirmacao());
    }

    private System.Collections.IEnumerator EsperarConfirmacao()
    {
        // 🔹 Espera 0.2s pra evitar detectar o mesmo "Z" usado pra abrir o baú
        aguardandoEntrada = true;
        yield return new WaitForSeconds(0.2f);
        aguardandoEntrada = false;

        // 🔹 Espera o jogador apertar Z só depois disso
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

        panel.SetActive(false);
    }
}
