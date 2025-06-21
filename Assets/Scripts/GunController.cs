using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject cannonballPrefab;

    public void FireAt(Vector3 target, Tile tile)
    {
        GameObject cannonballGO = Instantiate(cannonballPrefab, muzzlePoint.position, Quaternion.identity);
        Cannonball cannonball = cannonballGO.GetComponent<Cannonball>();
        cannonball.SetTarget(target + Vector3.up * 0.5f, tile);
    }
}
