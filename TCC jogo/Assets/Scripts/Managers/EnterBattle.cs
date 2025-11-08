using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public void StartBattle()
    {
        GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            GameManager.Instance.lastPlayerPosition = player.transform.position;

        SceneManager.LoadScene("BattleScene");
    }
}
