using UnityEngine;
using UnityEngine.UI;

public class FalaInicial3 : MonoBehaviour
{


    public GameObject telaInicial_3;
    public GameObject telaInicial_4;
    public Button Okay3;


    private void Awake()
    {
        
        if (telaInicial_3 == true)
        {
            Okay3.onClick.AddListener(() => telaInicial_4.SetActive(true));
            Okay3.onClick.AddListener(() => telaInicial_3.SetActive(false));
        }
    }

}
