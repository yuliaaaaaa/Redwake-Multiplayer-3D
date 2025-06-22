using UnityEngine;

[RequireComponent(typeof(Tile))]
public class EnemyTileClickHandler : MonoBehaviour
{
    private GunController gun;
    private Tile tile;

    void Awake()
    {
        tile = GetComponent<Tile>();
        var obj = GameObject.FindGameObjectWithTag("PlayerCannon");
        if (obj != null)
            gun = obj.GetComponent<GunController>();
    }
    void OnMouseDown()
    {
        if (!tile.IsEnemyField || tile.IsHit) return;

        GameManager.Instance.OnPlayerClick(tile);
    }

}
