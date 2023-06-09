using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la entrada del jugador en los ejes horizontal y vertical
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        //animator.SetFloat("Speed", Mathf.Abs( moveHorizontal), Mathf.Abs(moveVertical));
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(moveHorizontal), Mathf.Abs(moveVertical)));


        // Calcular el vector de movimiento
        movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // Rotar el jugador hacia la izquierda o la derecha
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(-1.3094f, 1.3094f, 1.3094f);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(1.3094f, 1.3094f, 1.3094f);
        }
    }

    private void FixedUpdate()
    {
        // Mover al jugador utilizando el Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
