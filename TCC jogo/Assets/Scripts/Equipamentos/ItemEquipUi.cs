using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ItemEquipUI : MonoBehaviour
{
    public Image iconImage;          // Ícone do item
    public TMP_Text nomeText;        // Nome do item
    

    private UnityAction onClick;

    public void Configurar(Item item, UnityEngine.Events.UnityAction acao)
    {
        if (iconImage != null)
        {
            if (item.icone != null)
            {
                iconImage.sprite = item.icone;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.sprite = null; // deixa vazio mas mantém visível
                iconImage.enabled = false; // ou pode deixar true se quiser sempre mostrar
            }
        }

        if (nomeText != null)
            nomeText.text = item.nome;

        

        onClick = acao;
    }


    // Para usar no botão do prefab
    public void Selecionar()
    {
        onClick?.Invoke();
    }
}
