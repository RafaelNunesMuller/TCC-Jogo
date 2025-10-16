using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    public static ItemSlot Instance;
    public Sprite itemSprite;
    public Button itemImageGO; 
    public string itemName;
    [TextArea] public string itemDescription;
    public int itemPrice;


    
    public Button itemButton;
    public ItemInfoUI infoUI;
    public Item itemDentro;

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
            infoUI.itemPrice = itemPrice;
            infoUI.ShowItem(itemSprite, itemName, itemDescription, itemPrice, this);
            itemImageGO.gameObject.SetActive(true);
    }
}
