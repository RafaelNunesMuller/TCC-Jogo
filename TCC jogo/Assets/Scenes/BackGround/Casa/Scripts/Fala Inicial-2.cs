using UnityEngine;
using UnityEngine.UI;

public class FalaInicial2 : MonoBehaviour
{


    public GameObject telaInicial_2;
    public GameObject telaInicial_3;
 
    public Button Okay2;

    private void Awake()
    {
        if (telaInicial_2 == true)
        {
            Okay2.onClick.AddListener(() =>  telaInicial_3.SetActive(true));
            Okay2.onClick.AddListener(() => telaInicial_2.SetActive(false));

        }
    }

}
