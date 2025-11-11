using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;
    private Animator anim;
    private Rigidbody2D rb;
    public bool canMove = true;


    public LayerMask Perigolayer;
    public int minSteps = 5;
    public int maxSteps = 15;
    private int stepsToNextEncounter;
    public float stepDistance = 0.5f;
    private Vector2 lastStepPosition;


    public playerStats playerStats;

    void Awake()
    {
        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        if (players.Length > 1)
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);


        if (GameManager.Instance != null)
        {
            var stats = GetComponent<playerStats>();
            if (stats != null)
                GameManager.Instance.playerStats = stats;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stepsToNextEncounter = Random.Range(minSteps, maxSteps + 1);
        lastStepPosition = rb.position;

 
        if (GameManager.Instance != null && GameManager.Instance.lastPlayerPosition != Vector3.zero)
        {
            transform.position = GameManager.Instance.lastPlayerPosition;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        var sr = GetComponent<SpriteRenderer>();

        if (cena.name == "Battle" || cena.name == "Compras" || cena.name == "Game Over" || cena.name == "Tela de vitória" || cena.name == "Compras (Masmorra)")
        {
            if (sr != null) sr.enabled = false;
        }
        else
        {
            if (sr != null) sr.enabled = true;
        }
    }


    void Update()
    {
        if (!canMove) return;

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement.x != 0) movement.y = 0;

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetBool("isMoving", movement != Vector2.zero);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("LastMovex", movement.x);
            anim.SetFloat("LastMovey", movement.y);

            float dist = Vector2.Distance(rb.position, lastStepPosition);
            if (dist >= stepDistance)
            {
                Collider2D perigo = Physics2D.OverlapCircle(transform.position, 0.2f, Perigolayer);
                if (perigo != null) HandleStep();
                lastStepPosition = rb.position;
            }
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

 
        switch (tag)
        {
            case "SaidaDungeon":
                SalvarPosicaoENovaCena("Mapa", new Vector3(4.48f, 3.92f, 0f));
                break;

            case "SaidaLoja":
                SalvarPosicaoENovaCena("Mapa", new Vector3(2.49f, -5.67f, 0f));
                break;

            case "SairDaCasa":
                SalvarPosicaoENovaCena("Mapa", new Vector3(-8.52f, 3.27f, 0f));
                break;

            case "IrParaCasa":
                SalvarPosicaoENovaCena("Casa", new Vector3(1.38f, 0.47f, 0f));
                break;

            case "IrParaLoja":
                SalvarPosicaoENovaCena("Lojinha", new Vector3(6f, -4f, 0f));
                break;

            case "SairDaLoja":
                SalvarPosicaoENovaCena("Mapa", new Vector3(2f, -6f, 0f));
                break;

            case "andar2":
                SalvarPosicaoENovaCena("Dungeon - sala 2", new Vector3(-13.51f, 8.5f, 0f));
                break;

            case "andar3":
                SalvarPosicaoENovaCena("Dungeon - sala_3", new Vector3(-13.5f, 7.45f, 0f));
                break;

            case "andar4":
                SalvarPosicaoENovaCena("Dungeon - sala 4", new Vector3(-2.51f, 9.89f, 0f));
                break;

            case "andar5":
                SalvarPosicaoENovaCena("Dungeon - sala 5", new Vector3(-19.5f, 8.57f, 0f));
                break;

            case "descanso":
                SalvarPosicaoENovaCena("Dungeon - Sala de Descanso", new Vector3(-17.49f, -0.19f, 0f));
                break;

            case "ultima":
                SalvarPosicaoENovaCena("Dungeon - O Fim", new Vector3(-23.46f, 0.44f, 0f));
                break;
        }
    }

    private void SalvarPosicaoENovaCena(string novaCena, Vector3 novaPosicao)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;
            GameManager.Instance.lastPlayerPosition = novaPosicao;
        }

        SceneManager.LoadScene(novaCena);
    }
}
