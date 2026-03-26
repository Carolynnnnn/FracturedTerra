using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (move > 0)
        {
            spriteRenderer.flipX = false; // face right
        }
        else if (move < 0)
        {
            spriteRenderer.flipX = true; // face left
        }
    }
}