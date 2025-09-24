using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public string nomeItem;
    [TextArea] public string descricaoItem;
    public Sprite iconeItem;

    public PainelItemUI painelInfoUI; // arraste o painel no inspector

    public void AoClicar()
    {
        painelInfoUI.AbrirPainel(nomeItem, descricaoItem, iconeItem);
    }
}
