using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveDistance = 2f;

    private Vector3 startPos;
    private bool movingRight = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }
}