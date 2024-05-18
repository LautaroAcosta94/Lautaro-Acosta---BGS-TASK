using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad de movimiento del personaje
    public float moveSpeed = 5f;

    // Referencia al componente Rigidbody2D del personaje
    private Rigidbody2D rb;

    // Vector para almacenar el input del movimiento
    private Vector2 movement;

    // Almacena la escala original del personaje para revertir fácilmente la rotación
    private Vector3 originalScale;

    // Referencia al componente Animator del personaje
    public Animator animator;

    void Start()
    {
        // Obtén el componente Rigidbody2D del objeto al que está adjunto este script
        rb = GetComponent<Rigidbody2D>();

        // Almacena la escala original del personaje
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Captura el input de las teclas W, A, S, D
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normaliza el vector de movimiento para que la velocidad sea consistente
        movement = movement.normalized;

        // Turns the character based on the direction of the horizontal movement
        if (movement.x < 0)
        {
            // It turns to the left
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if (movement.x > 0)
        {
            // It turns to the right
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        // This is for the character animation controller
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Movement", 0.5f);
        }
        else
        {
            animator.SetFloat("Movement", 0f);
        }
    }

    void FixedUpdate()
    {
        // Aplica el movimiento al Rigidbody2D del personaje
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
