using UnityEngine;
using UnityEngine.UI;

public class FalaInicial4 : MonoBehaviour
{

    public GameObject telaInicial_4;

    public Button Okay4;

    private void Awake()
    {

        if (telaInicial_4 == true)
        {
            Okay4.onClick.AddListener(() => telaInicial_4.SetActive(false));
        }
    }

}
