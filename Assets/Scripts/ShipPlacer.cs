/*using UnityEngine;

public class ShipPlacer : MonoBehaviour
{
    private Camera cam;
    private bool isDragging = false;
    private GridGenerator grid;
    public bool IsDragging => isDragging;

    public void BeginDrag()
    {
        isDragging = true;
        Debug.Log("✅ Почали перетягування корабля");
    }

    void Start()
    {
        cam = Camera.main;
        grid = FindObjectOfType<GridGenerator>();
        Debug.Log("🧩 ShipPlacer стартував");
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 screenPos = Input.mousePosition;
        Debug.Log("🖱 mouse screen pos: " + screenPos);

        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0;
        transform.position = worldPos;

        Debug.Log("🌍 Корабель тепер на: " + worldPos);
    }

    public void PlaceOn(Tile targetTile)
    {
        if (targetTile == null || targetTile.IsOccupied || targetTile.IsEnemyField)
        {
            Debug.Log("❌ Неможливо поставити — тайл зайнятий або вороже поле");
            Destroy(gameObject);
            return;
        }

        Ship ship = GetComponent<Ship>();
        if (ship != null)
        {
            ship.PlaceOnTiles(targetTile, grid.Grid);
        }

        isDragging = false;
        Debug.Log($"✅ Корабель поставлено вручну на {targetTile.GridPosition}");
    }
}
*/