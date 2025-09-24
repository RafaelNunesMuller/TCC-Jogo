using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

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

    public GameObject menuUI;
    public GameObject MenuPanel;
    public bool canMove = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stepsToNextEncounter = Random.Range(minSteps, maxSteps + 1);
        lastStepPosition = rb.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SairDaCasa"))
        {
            SceneManager.LoadScene("Mapa");
        }
    }

    void Update()
    {
        if (!canMove)
        {
            anim.SetBool("isMoving", false);
            rb.linearVelocity = Vector2.zero;
            return;
        }

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

        Debug.Log($"Passo contado. Restam {stepsToNextEncounter}");

        if (stepsToNextEncounter <= 0)
        {
            if (Random.Range(1, 101) <= 100) // % de chance de encontrar uma batalha pode ser ajustado pelo codigo
            {
                Debug.Log("⚔️ Encontro de batalha!");
                SceneManager.LoadScene("Battle");
            }

            stepsToNextEncounter = Random.Range(minSteps, maxSteps + 1);
            Debug.Log($"Novo contador: {stepsToNextEncounter}");
        }
    }
}
