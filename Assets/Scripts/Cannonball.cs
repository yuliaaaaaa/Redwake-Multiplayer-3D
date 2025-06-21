using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private Vector3 targetPosition;
    private Tile targetTile;
    public float speed = 5f;
    private bool isMoving = false;

    public void SetTarget(Vector3 target, Tile tile)
    {
        targetPosition = target;
        targetTile = tile;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Hit();
        }
    }
    void Hit()
    {
        isMoving = false;

        if (targetTile != null)
        {
            targetTile.OnHit();
            GameManager.Instance.OnCannonballHitCompleted(targetTile);
        }

        Destroy(gameObject, 0.1f);
    }

}