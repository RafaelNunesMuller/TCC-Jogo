using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Ver se consegue arrumar o posicionamento quando ele entra e sai de um local, pois está torto

    public float moveSpeed = 5f;
    public playerStats playerStats;

    private Vector2 movement;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Encontros")]
    public LayerMask Perigolayer;
    public int minSteps = 5;
    public int maxSteps = 15;
    private int stepsToNextEncounter;
    public float stepDistance = 0.5f; // distância que conta como 1 passo
    private Vector2 lastStepPosition;
    public bool canMove = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stepsToNextEncounter = Random.Range(minSteps, maxSteps + 1);
        lastStepPosition = rb.position;

        if (GameManager.Instance != null && GameManager.Instance.playerStats != null)
        {
            playerStats = GameManager.Instance.playerStats;
            Debug.Log(" Player pegou o playerStats do GameManager.");
        }


    }

    void Awake()
    {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);

        if (players.Length > 1)
        {
            
            Destroy(gameObject);
            return;
        }

        

        //  Garante que o GameManager tenha referência ao playerStats real
        if (GameManager.Instance != null)
        {
            var stats = GetComponent<playerStats>();
            if (stats != null)
            {
                GameManager.Instance.playerStats = stats;
            }
        }
    }

    void OnEnable()
    {
        // Quando a cena de batalha for carregada, checa se é "Battle"
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        if (cena.name == "Battle")
        {
            // 🔹 Deixa o player invisível
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = false;

            // 🔹 Opcional: desativa o colisor também
            var col = GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
        }
        else
        {
            // 🔹 Quando voltar pro mapa, reativa o player
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = true;

            var col = GetComponent<Collider2D>();
            if (col != null)
                col.enabled = true;
        }
    }



    void Update()
    {


        if (!canMove) return;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Se está se movendo, salva a posição atual
        

        // Movimento
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement.x != 0) movement.y = 0;

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("LastMovex", movement.x);
            anim.SetFloat("LastMovey", movement.y);

            // Checa se percorreu distância suficiente para ser um "passo"
            float dist = Vector2.Distance(rb.position, lastStepPosition);
            if (dist >= stepDistance)
            {
                Collider2D perigo = Physics2D.OverlapCircle(transform.position, 0.2f, Perigolayer);
                if (perigo != null)
                {
                    HandleStep();
                }

                lastStepPosition = rb.position;
            }
        }

        anim.SetBool("isMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleStep()
    {
        stepsToNextEncounter--;

        if (stepsToNextEncounter <= 0)
        {
            if (Random.Range(1, 101) <= 35)
            {
                Debug.Log(" Encontro de batalha!");

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;
                    GameManager.Instance.lastPlayerPosition = transform.position;
                }

                SceneManager.LoadScene("Battle");
            }

            stepsToNextEncounter = Random.Range(minSteps, maxSteps + 1);
        }
    }


}
