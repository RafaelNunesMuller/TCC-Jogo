using UnityEngine;
using System.Collections;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector2 movement;
    private Vector3 targetPos;
    private Animator anim;
    public GameObject menuUI;
    public GameObject MenuPanel;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetPos = transform.position;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I)) // ou ESC, ou outro bot�o
        {
            menuUI.SetActive(menuUI.activeSelf); // ativa/desativa
        }


        if (!isMoving)
        {
            // Captura da dire��o
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Travar em uma dire��o por vez (como Pok�mon e Mother fazem)
            if (movement.x != 0)
                movement.y = 0;

            if (movement != Vector2.zero)
            {
                // Calcula o pr�ximo bloco
                targetPos = transform.position + new Vector3(movement.x, movement.y, 0);
                StartCoroutine(Move());

                // Atualiza dire��o e "�ltima dire��o"
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
                anim.SetFloat("LastMovex", movement.x);
                anim.SetFloat("LastMovey", movement.y);
            }
        }

        // Enquanto estiver andando, manter a anima��o ativa
        anim.SetBool("isMoving", isMoving);
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
