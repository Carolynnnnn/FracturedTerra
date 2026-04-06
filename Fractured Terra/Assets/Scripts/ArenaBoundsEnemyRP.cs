using UnityEngine;

public class ArenaBoundsEnemyRP : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY; // defines the box the enemy is allowed to move in

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // clamps enemy position so it can’t leave the arena (prevents it from wandering off screen)
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}