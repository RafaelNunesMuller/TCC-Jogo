using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector2 input;
    private Vector3 targetPos;
    private Animator anim;

    private Vector2 lastMoveDir = Vector2.down; // Começa virado pra baixo

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Impede diagonais
            if (input != Vector2.zero)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                    input.y = 0;
                else
                    input.x = 0;

                // Salva última direção
                lastMoveDir = input;

                // Atualiza animação
                anim.SetFloat("Horizontal", input.x);
                anim.SetFloat("Vertical", input.y);
                anim.SetFloat("Speed", 1f);

                targetPos = transform.position + new Vector3(input.x, input.y, 0);
                StartCoroutine(Move());
            }
            else
            {
                anim.SetFloat("Speed", 0f);

                // Mesmo parado, mantém a direção do idle
                anim.SetFloat("Horizontal", lastMoveDir.x);
                anim.SetFloat("Vertical", lastMoveDir.y);
            }
        }
    }

    IEnumerator Move()
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
