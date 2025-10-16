using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TMP_Text coinsText;
    public int Coins = 0;
    public GameObject NoCoins;
    public Button OkayCoins;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // tenta localizar automaticamente os objetos necessários
        if (coinsText == null)
        {
            GameObject coinsObj = GameObject.Find("Mostrar Coins");
            if (coinsObj != null)
                coinsText = coinsObj.GetComponent<TMP_Text>();
        }

        if (NoCoins == null)
        {
            NoCoins = GameObject.Find("NoCoins");
        }

        if (OkayCoins == null && NoCoins != null)
        {
            Transform button = NoCoins.transform.Find("OkayCoins");
            if (button != null)
                OkayCoins = button.GetComponent<Button>();
        }

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

    public bool TrySpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            ShowCoins(Coins);
            return true;
        }
        else
        {
            // Se estiver na cena da loja, tenta abrir o painel de "sem moedas"
            if (NoCoins == null)
                NoCoins = GameObject.Find("NoCoins");

            if (NoCoins != null)
            {
                NoCoins.SetActive(true);

                if (OkayCoins == null)
                {
                    Transform button = NoCoins.transform.Find("OkayCoins");
                    if (button != null)
                        OkayCoins = button.GetComponent<Button>();
                }

                if (OkayCoins != null)
                {
                    OkayCoins.onClick.RemoveAllListeners();
                    OkayCoins.onClick.AddListener(() => NoCoins.SetActive(false));
                }
            }

            Debug.Log("Moedas insuficientes!");
            return false;
        }
    }
}
