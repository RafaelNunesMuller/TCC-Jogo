// ItemInfoUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Interfaces;

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
    public GameObject exit;
    public Button IndButton;
    public GameObject NoCoins;
    public Button OkayCoins;
    public GameObject Vendedor;
    public Button Okay;
    public int itemPrice;
    public GameObject player;

    public static Inventario instance;
    public static CoinManager Coins;


    private int currentItemPrice;
    private ItemSlot currentItemSlot;


    private void Awake()
    {
        itens.SetActive(true);
        panel.SetActive(false);
        exit.gameObject.SetActive(true);

        exitButton.onClick.AddListener(() => panel.SetActive(false));
        exitButton.onClick.AddListener(() => itens.SetActive(true));
        exitButton.onClick.AddListener(() => exit.SetActive(true));

        buyButton.onClick.RemoveAllListeners(); 
        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {

        if (currentItemSlot == null) return;

        int price = currentItemSlot.itemPrice;
        if (CoinManager.instance.TrySpendCoins(price) &&  !currentItemSlot.isPurchased)
        {
            Debug.Log("Item comprado!");
            currentItemSlot.isPurchased = true;
            Inventario.instance.Adicionar(currentItemSlot.itemDentro);
           

            panel.SetActive(false);
            itens.SetActive(false);
            Vendedor.SetActive(true);
            Okay.gameObject.SetActive(true);

            Okay.onClick.RemoveAllListeners();
            Okay.onClick.AddListener(() => Vendedor.SetActive(false));
            Okay.onClick.AddListener(() => itens.SetActive(true));
            Okay.onClick.AddListener(() => exit.SetActive(true));
        }
        else
        {
            NoCoins.gameObject.SetActive(true);
            OkayCoins.onClick.AddListener(() => NoCoins.SetActive(false));
        }
    }





    public void ShowItem(Sprite sprite, string name, string description, int price, ItemSlot slot)
    {

        currentItemSlot = slot;
        
        if (currentItemSlot.isPurchased == true)
        {
            buyButton.gameObject.SetActive(false);
            IndButton.gameObject.SetActive(true);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            IndButton.gameObject.SetActive(false);
        }

        
        currentItemPrice = price;
        itens.SetActive(false);
        panel.SetActive(true);
        exit.gameObject.SetActive(false);
     
        itemImage.sprite = sprite;
        itemNameText.text = name;
        itemDescriptionText.text = description;
        itemPriceText.text = "R$ " + price;


        itemNameText.gameObject.SetActive(true);
        itemDescriptionText.gameObject.SetActive(true);
        itemPriceText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

    }




}

