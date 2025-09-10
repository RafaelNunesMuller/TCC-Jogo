// DialogueTypewriter.cs
using UnityEngine;
using TMPro;        // troque pra UnityEngine.UI e Text se NÃO usar TextMeshPro
using System.Collections;

public class DialogueTypewriter : MonoBehaviour
{
    public TMP_Text uiText;           // arrasta aqui o TextMeshPro - Text (ou mude o tipo para Text)
    [TextArea(2, 6)]
    public string[] lines;            // linhas do diálogo
    public float typeSpeed = 0.03f;   // tempo entre caracteres

    private int index = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (uiText == null) Debug.LogError("DialogueTypewriter: arraste o TMP_Text no inspector.");
        if (lines == null || lines.Length == 0) return;
        uiText.text = "";
        StartTypingLine(index);
    }

    void Update()
    {
        // clique do mouse (PC) ou toque (mobile)
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (isTyping)
            {
                // termina a digitação instantaneamente
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                uiText.text = lines[index];
                isTyping = false;
            }
            else
            {
                // avança para a próxima linha
                index++;
                if (index < lines.Length)
                {
                    StartTypingLine(index);
                }
                else
                {
                    // terminou o diálogo
                    Debug.Log("Dialogue finished");
                    // ex.: uiText.gameObject.SetActive(false);
                }
            }
        }
    }

    void StartTypingLine(int i)
    {
        typingCoroutine = StartCoroutine(TypeText(lines[i]));
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        uiText.text = "";
        foreach (char c in line)
        {
            uiText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }
}
