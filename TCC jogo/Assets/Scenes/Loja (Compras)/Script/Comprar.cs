using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Comprar : MonoBehaviour
{

    public Button buyButton;

    private void Awake()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    /private void BuyItem()
    {
        Debug.Log("Item comprado!");
        //buyButton.gameObject.SetActive(false);
        Vendedor.SetActive(true);
        Okay.gameObject.SetActive(true);
        Okay.onClick.AddListener(() => Vendedor.SetActive(false));
        
    }
}
