using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Indisponível : MonoBehaviour
{
    public GameObject panel;
    public GameObject itens;
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemPriceText;
    public Button IndButton;
    public Button exitButton;
    public GameObject exit;
    public GameObject Vendedor;
    public Button Okay;



    private void Awake()
    {

        itens.SetActive(true);
        panel.SetActive(false);
        exit.gameObject.SetActive(true);

        exitButton.onClick.AddListener(() => panel.SetActive(false));
        exitButton.onClick.AddListener(() => itens.SetActive(true));
        exitButton.onClick.AddListener(() => exit.SetActive(true));
        IndButton.onClick.AddListener(IndItem);

    }

    private void IndItem()
    {
        Vendedor.SetActive(true);
        Okay.gameObject.SetActive(true);
        Okay.onClick.AddListener(() => Vendedor.SetActive(false));
        
        
    }

    public void ShowItem(Sprite sprite, string name, string description, int price)
    {
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
        IndButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }




}
