using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Indisponível : MonoBehaviour
{

    public GameObject panel;
    public TMP_Text descrição;
    [TextArea] public string itemDescription;

    private void Awake()
    {
        panel.SetActive(false);
    }


    public void ShowItem(string itemDescription)
    {
        panel.SetActive(true);
        descrição.gameObject.SetActive(true);
    }
}
