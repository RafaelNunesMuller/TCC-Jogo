using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TMP_Text coinsText;
    public int Coins = 0;
    public Button buyButton;
    public int itemPrice;
    public GameObject NoCoins;
    public Button OkayCoins;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // escuta trocas de cena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // tenta encontrar o novo texto automaticamente
        if (coinsText == null)
            coinsText = GameObject.Find("Mostrar Coins").GetComponent<TMP_Text>();

        ShowCoins(Coins);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        ShowCoins(Coins);
    }

    public void ShowCoins(int Coins)
    {
        if (coinsText != null)
            coinsText.text = "R$ " + Coins;
    }
}
