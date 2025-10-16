using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;      // Imagem vermelha de tela cheia
    public float flashDuration = 0.2f; // tempo do flash
    public float fadeSpeed = 5f;  // velocidade do fade

    private Color targetColor;

    void Start()
    {
        if (flashImage == null)
            flashImage = GetComponent<Image>();

        // Começa totalmente transparente
        flashImage.color = new Color(1f, 0f, 0f, 0f);
    }

    void Update()
    {
        // Faz o alpha voltar para 0 suavemente
        flashImage.color = Color.Lerp(
            flashImage.color,
            new Color(1f, 0f, 0f, 0f),
            fadeSpeed * Time.deltaTime
        );
    }

    public void Flash()
    {
        flashImage.color = new Color(1f, 0f, 0f, 0.5f);
    }
}
