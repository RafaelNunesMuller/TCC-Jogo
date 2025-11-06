using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ItemEquipUI : MonoBehaviour
{
    public Image iconImage;          
    public TMP_Text nomeText;        
    

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
                iconImage.sprite = null;
                iconImage.enabled = false;
            }
        }

        if (nomeText != null)
            nomeText.text = item.nome;

        

        onClick = acao;
    }


    public void Selecionar()
    {
        onClick?.Invoke();
    }
}
