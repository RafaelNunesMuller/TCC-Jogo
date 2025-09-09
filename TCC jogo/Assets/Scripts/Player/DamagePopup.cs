using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TMP_Text text;
    private float moveSpeed = 1f;
    private float disappearTime = 1f;
    private Color textColor;

    public void Setup(int damageAmount)
    {
        text.text = damageAmount.ToString();
        textColor = text.color;
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            textColor.a -= 2f * Time.deltaTime;
            text.color = textColor;
            if (textColor.a <= 0) Destroy(gameObject);
        }
    }
}
