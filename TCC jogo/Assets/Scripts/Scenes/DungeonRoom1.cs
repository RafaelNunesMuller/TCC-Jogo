using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonRoom1 : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite closedSprite;
    public Sprite openSprite;

    [Header("Configura��o")]
    public string nextSceneName; // nome da cena a carregar

    private bool isOpen = false;
    private SpriteRenderer sr;
    public GameObject Player;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closedSprite; // come�a fechada
    }


    // Se quiser que o player abra ao apertar Z enquanto encosta:
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isOpen && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
        {
            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            if (camShake != null)
            {
                StartCoroutine(camShake.Shake(0.2f, 0.2f)); // dura��o, intensidade
            }
            AbrirPorta();

        }
    }

    void AbrirPorta()
    {
        isOpen = true;
        sr.sprite = openSprite;
        
        Debug.Log("Porta aberta!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // S� troca de cena se j� estiver aberta
        if (isOpen && other.CompareTag("Player"))
        {
            Player.transform.position = new Vector3(-1.45f, 7.52f);
            SceneManager.LoadScene(nextSceneName);
            
        }
    }
}
