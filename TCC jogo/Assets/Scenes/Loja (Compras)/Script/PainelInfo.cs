// ItemInfoUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoUI : MonoBehaviour
{
    public GameObject panel;
    public GameObject itens;
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemPriceText;
    public Button buyButton;
    public Button exitButton;

    private void Awake()
    {
        itens.SetActive(true);
        panel.SetActive(false);
        
        exitButton.onClick.AddListener(() => panel.SetActive(false));
        exitButton.onClick.AddListener(() => itens.SetActive(true));
        
    }

    public void ShowItem(Sprite sprite, string name, string description, int price)
    {
        itens.SetActive(false);
        panel.SetActive(true);
     
        itemImage.sprite = sprite;
        itemNameText.text = name;
        itemDescriptionText.text = description;
        itemPriceText.text = "Preço: " + price;


        itemNameText.gameObject.SetActive(true);
        itemDescriptionText.gameObject.SetActive(true);
        itemPriceText.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);



    }
}
