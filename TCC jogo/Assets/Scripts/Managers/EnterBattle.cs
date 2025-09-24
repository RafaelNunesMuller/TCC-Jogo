using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattle : MonoBehaviour
{
    public void StartBattle()
    {
        GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;
        GameManager.Instance.lastPlayerPosition =
            GameObject.FindWithTag("Player").transform.position;

        SceneManager.LoadScene("BattleScene");
    }
}
