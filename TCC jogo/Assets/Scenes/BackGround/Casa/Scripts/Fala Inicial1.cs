using UnityEngine;
using UnityEngine.UI;

public class FalaInicial1 : MonoBehaviour
{

    public GameObject telaInicial_1;
    public GameObject telaInicial_2;
    public Button Okay1;


    private void Awake()
    {
        if (telaInicial_1 == true)
        {
            Okay1.onClick.AddListener(() => telaInicial_2.SetActive(true));
            Okay1.onClick.AddListener(() => telaInicial_1.SetActive(false));
            
        }
    }
}
