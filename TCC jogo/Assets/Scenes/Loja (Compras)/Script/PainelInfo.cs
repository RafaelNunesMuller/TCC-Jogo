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
    public Button unavailableButton;
    public GameObject exit;


    private void Awake()
    {   
        
        itens.SetActive(true);
        panel.SetActive(false);
        exit.gameObject.SetActive(true);
  
        exitButton.onClick.AddListener(() => panel.SetActive(false));
        exitButton.onClick.AddListener(() => itens.SetActive(true));
        exitButton.onClick.AddListener(() => exit.SetActive(true));

        buyButton.onClick.AddListener(BuyItem);

    }

    private void BuyItem()
    {
        Debug.Log("Item comprado!");
        // Aqui você pode colocar lógica de inventário, descontar moedas, etc.
        panel.SetActive(false);
        itens.SetActive(true);
        exit.gameObject.SetActive(true);
    }

    public void ShowItem(Sprite sprite, string name, string description, int price)
    {
        itens.SetActive(false);
        panel.SetActive(true);
        exit.gameObject.SetActive(false);
     
        itemImage.sprite = sprite;
        itemNameText.text = name;
        itemDescriptionText.text = description;
        itemPriceText.text = "Preço: " + price;


        itemNameText.gameObject.SetActive(true);
        itemDescriptionText.gameObject.SetActive(true);
        itemPriceText.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
      

        exitButton.gameObject.SetActive(true);
    }




}

