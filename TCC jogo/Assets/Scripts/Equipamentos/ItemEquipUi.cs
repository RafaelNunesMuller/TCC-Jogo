using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemEquipUI : MonoBehaviour
{
    public TMP_Text nomeText;
    public Button button;
    private System.Action acao;

    public void Configurar(Item item, System.Action onClick)
    {
        nomeText.text = item.nome;
        acao = onClick;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => acao.Invoke());
    }

    public void Acionar()
    {
        acao?.Invoke();
    }

    public void SetColor(Color cor)
    {
        nomeText.color = cor;
    }
}
