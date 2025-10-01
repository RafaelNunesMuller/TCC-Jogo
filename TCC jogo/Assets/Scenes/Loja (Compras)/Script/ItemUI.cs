using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Sprite itemSprite;
    public Button itemImageGO; 
    public string itemName;
    [TextArea] public string itemDescription;
    public int itemPrice;



    public Button itemButton;     // botão do item
    public ItemInfoUI infoUI;      // referência ao painel

    private void Awake()
    {
        if (itemButton == null) itemButton = GetComponent<Button>();
        if (infoUI == null) infoUI = Object.FindFirstObjectByType<ItemInfoUI>();

        if (itemButton != null)
            itemButton.onClick.AddListener(OpenPanel);
    }

    public void OpenPanel()
    {
        if (infoUI != null)
            infoUI.ShowItem(itemSprite, itemName, itemDescription, itemPrice);
            itemImageGO.gameObject.SetActive(true);
    }
}
