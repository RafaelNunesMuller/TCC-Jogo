using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCasa : MonoBehaviour
{

    private int Coinprat = 100;
    public bool foiAberto = false;
    private bool playerPerto = false;
    public CoinManager Coins;
    public GameObject FalaPlayer;
    public Button ok;





    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
        }
            
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerPerto = false;
    }

    void Update()
    {
        if (playerPerto && !foiAberto && Input.GetKeyDown(KeyCode.Z))
        {
            AddCoins();
            FalaPlayer.SetActive(true);
            ok.onClick.AddListener(() => FalaPlayer.SetActive(false));
        }
    }
    private void AddCoins()
    {
        Coins.Coins += Coinprat;
        foiAberto = true;

        if (Coins != null)
            Coins.ShowCoins(Coins.Coins);
            
    }

    
}
