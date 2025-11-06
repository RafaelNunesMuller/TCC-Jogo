using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonRoom1 : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    public string nextSceneName;

    private bool isOpen = false;
    private SpriteRenderer sr;
    public GameObject Player;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closedSprite;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isOpen && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
        {
            CameraShake camShake = Camera.main.GetComponent<CameraShake>();
            if (camShake != null)
            {
                StartCoroutine(camShake.Shake(0.2f, 0.2f));
            }
            AbrirPorta();

        }
    }

    void AbrirPorta()
    {
        isOpen = true;
        sr.sprite = openSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        if (!isOpen) return;
        if (!other.CompareTag("Player")) return;

        // salva posição e cena destino no GameManager
        GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;
        GameManager.Instance.lastPlayerPosition = new Vector3(-1.45f, 7.52f, 0f);

        SceneManager.LoadScene(nextSceneName);
=======
        if (isOpen && other.CompareTag("Player"))
        {
            Player.transform.position = new Vector3(-1.45f, 7.52f);
            SceneManager.LoadScene(nextSceneName);
            
        }
>>>>>>> Ale
    }

}
