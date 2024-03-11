using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 5f;
    public float amplitude = 2f; // Amplitud del movimiento vertical
    public float frequency = 1f; // Frecuencia del movimiento vertical

    public Sprite spriteUp;
    public Sprite spriteMiddle;
    public Sprite spriteDown;

    private bool movingRight = true;
    private float startingY; // Posición inicial en el eje Y
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startingY = transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mueve el pájaro de un lado a otro
        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveDirection * speed * Time.deltaTime);

        // Calcula la posición vertical usando una función sinusoidal
        float newY = startingY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Cambia la dirección cuando llega al límite
        if (transform.position.x >= distance && movingRight)
        {
            Flip();
        }
        else if (transform.position.x <= -distance && !movingRight)
        {
            Flip();
        }

        // Cambia el sprite dependiendo de la posición vertical
        if (newY > startingY)
        {
            spriteRenderer.sprite = spriteUp;
        }
        else if (newY < startingY)
        {
            spriteRenderer.sprite = spriteDown;
        }
        else
        {
            spriteRenderer.sprite = spriteMiddle;
        }
    }

    void Flip()
    {
        // Cambia la dirección del pájaro
        movingRight = !movingRight;

        // Voltea la escala en el eje X para invertir la dirección del sprite
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
