using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    public float fadeSpeed = 5f;

    private Color targetColor;

    void Start()
    {
        if (flashImage == null)
            flashImage = GetComponent<Image>();

        flashImage.color = new Color(1f, 0f, 0f, 0f);
    }

    void Update()
    {
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
