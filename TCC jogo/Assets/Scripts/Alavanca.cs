using Unity.VisualScripting;
using UnityEngine;

public class Alavanca : MonoBehaviour
{

    public Sprite closedSprite;
    public GameObject PortaMovida;
    private bool isOpen = false;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closedSprite; // começa fechada
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isOpen && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.transform.Rotate(0, -181, 50f * Time.deltaTime);
            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            if (camShake != null)
            {
                StartCoroutine(camShake.Shake(0.2f, 0.2f)); // duração, intensidade
            }
            AbrirPorta();

        }
    }

    void AbrirPorta()
    {
        isOpen = true;
      
        PortaMovida.SetActive(false);
        Debug.Log("Porta aberta!");
    }
}
