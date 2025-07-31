using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade suave do movimento
    private bool isMoving = false;
    private Vector2 input;
    private Vector3 targetPos;

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Impede movimento diagonal
            if (input != Vector2.zero)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                    input.y = 0;
                else
                    input.x = 0;

                // Mover 1 unidade no mundo Unity (equivale a 1 tile de 16x16 se PPU = 16)
                targetPos = transform.position + new Vector3(input.x, input.y, 0);
                StartCoroutine(Move());
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
