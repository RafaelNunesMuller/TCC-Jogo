using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public void StartBattle()
    {



        // ✅ Salva cena atual
        GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;

        // ✅ Salva posição atual do player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            GameManager.Instance.lastPlayerPosition = player.transform.position;

        // ✅ Carrega cena de batalha
        SceneManager.LoadScene("BattleScene");
    }
}
