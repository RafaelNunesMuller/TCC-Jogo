using UnityEngine;
using UnityEngine.UI;

public class ItemInd : MonoBehaviour
{
    public Indispon�vel indUI; 
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public int itemPrice;
    public Button itemButton;

    private void Awake()
    {
        if (itemButton == null) itemButton = GetComponent<Button>();
        if (indUI == null) indUI = Object.FindFirstObjectByType<Indispon�vel>();

        if (itemButton != null)
            itemButton.onClick.AddListener(OpenPanel);
    }

    public void OpenPanel()
    {
        indUI.ShowItem(itemSprite, itemName, itemDescription, itemPrice);
    }
}
