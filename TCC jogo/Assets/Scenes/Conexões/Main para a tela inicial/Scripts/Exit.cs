using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Saindo do jogo...");

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
