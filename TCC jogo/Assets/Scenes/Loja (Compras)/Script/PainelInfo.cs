using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    [Header("Referências do UI")]
    public GameObject panelPopup;
    public GameObject panelFundoEscuro;
    public TMP_Text nomeText;
    public TMP_Text descricaoText;
    public TMP_Text precoText;
    public Button botaoComprar;
    public Button botaoSair;

    // Exemplo de método para abrir o popup
    public void AbrirPopup(string nome, string descricao, string preco)
    {
        panelFundoEscuro.SetActive(true);
        panelPopup.SetActive(true);

        nomeText.text = nome;
        descricaoText.text = descricao;
        precoText.text = preco;

        botaoComprar.gameObject.SetActive(true);
        botaoSair.gameObject.SetActive(true);

        // Configura botão sair para fechar
        botaoSair.onClick.RemoveAllListeners();
        botaoSair.onClick.AddListener(FecharPopup);
    }

    public void FecharPopup()
    {
        panelPopup.SetActive(false);
        panelFundoEscuro.SetActive(false);
        botaoComprar.gameObject.SetActive(false);
        botaoSair.gameObject.SetActive(false);
    }
}
