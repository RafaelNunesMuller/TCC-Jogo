using UnityEngine;
using UnityEngine.UI;

public class FalaInicial4 : MonoBehaviour
{

    public GameObject telaInicial_4;
    public GameObject telaInicial_5;

    public Button Okay4;

    private void Awake()
    {

        if (telaInicial_4 == true)
        {
            Okay4.onClick.AddListener(() => telaInicial_4.SetActive(false));
            Okay4.onClick.AddListener(() => telaInicial_5.SetActive(true));
        }
    }

}
