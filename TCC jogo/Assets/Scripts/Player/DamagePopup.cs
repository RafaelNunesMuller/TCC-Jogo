using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float fadeSpeed = 2f;
    private TMP_Text textMesh;
    private Color textColor;

    void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        textColor = textMesh.color;
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }

    void Update()
    {
        // movimento para cima
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // fade
        textColor.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = textColor;

        
    }
}
