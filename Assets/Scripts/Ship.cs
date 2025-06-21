/*using UnityEngine;

public class Ship : MonoBehaviour
{
    public int length = 3;
    public bool isVertical = false;
    private Tile[] occupiedTiles;

    public void PlaceOnTiles(Tile startTile, Tile[,] grid)
    {
        int x = startTile.GridPosition.x;
        int y = startTile.GridPosition.y;

        occupiedTiles = new Tile[length];

        for (int i = 0; i < length; i++)
        {
            int dx = isVertical ? 0 : i;
            int dy = isVertical ? i : 0;

            Tile tile = grid[x + dx, y + dy];
            tile.IsOccupied = true;
            occupiedTiles[i] = tile;
        }

        Vector3 center = Vector3.zero;
        foreach (Tile tile in occupiedTiles)
            center += tile.transform.position;
        center /= length;

        transform.position = center + new Vector3(0, 0.5f, 0);

        transform.rotation = isVertical ? Quaternion.Euler(0, 90, 0) : Quaternion.identity;
    }
}*/