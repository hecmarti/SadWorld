using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDance : MonoBehaviour
{
    public Sprite spriteLeft;
    public Sprite spriteRight;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeSprite", 1f, 1f); // Llama al método ChangeSprite cada segundo
    }

    // Método para cambiar el sprite
    void ChangeSprite()
    {
        if (spriteRenderer != null)
        {
            // Accede al sprite asignado
            Sprite sprite = spriteRenderer.sprite;

            // Cambia el sprite
            if (sprite != spriteLeft)
            {
                spriteRenderer.sprite = spriteLeft; // Asigna spriteLeft a spriteRenderer.sprite
            }
            else
            {
                spriteRenderer.sprite = spriteRight; // Asigna spriteRight a spriteRenderer.sprite
            }
        }
    }
}
