using UnityEngine;
using UnityEngine.UI;

public class Controles : MonoBehaviour
{

    public GameObject telaInicial_5;
    public Button Okay5;

    private void Awake()
    {
        if (telaInicial_5 == true)
        {
            Okay5.onClick.AddListener(() => telaInicial_5.SetActive(false));
        }
    }

}
