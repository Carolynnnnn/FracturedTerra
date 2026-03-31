using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public BoxCollider2D attackHitbox;
    public GameObject attackSpriteObject;
    public SpriteRenderer normalPlayerSprite;
    public float attackDuration = 0.2f;

    void Start()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;

        if (attackSpriteObject != null)
            attackSpriteObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack()
    {
        if (normalPlayerSprite != null)
            normalPlayerSprite.enabled = false;

        if (attackSpriteObject != null)
            attackSpriteObject.SetActive(true);

        if (attackHitbox != null)
            attackHitbox.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        if (attackHitbox != null)
            attackHitbox.enabled = false;

        if (attackSpriteObject != null)
            attackSpriteObject.SetActive(false);

        if (normalPlayerSprite != null)
            normalPlayerSprite.enabled = true;
    }
}