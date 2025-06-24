using System.Collections.Generic;
using UnityEngine;

public class ShipPlacementManager : MonoBehaviour
{
    public Transform enemyGridRoot;

    private Tile[,] grid = new Tile[10, 10];
    private bool[,] forbidden = new bool[10, 10];

    private List<Ship> enemyShips = new List<Ship>();
    public List<Ship> Ships => enemyShips;
    private class ShipDefinition
    {
        public int length;
        public int count;
        public ShipDefinition(int len, int cnt)
        {
            length = len;
            count = cnt;
        }
    }

    public void GenerateShips()
    {
        CacheGrid();
        PlaceAllShips();
    }

    void CacheGrid()
    {
        int count = 0;
        foreach (Tile tile in enemyGridRoot.GetComponentsInChildren<Tile>())
        {
            Vector2Int pos = tile.GridPosition;

            if (pos.x >= 0 && pos.x < 10 && pos.y >= 0 && pos.y < 10)
            {
                grid[pos.x, pos.y] = tile;
                count++;
            }
            else
            {
                Debug.LogWarning($"Tile має некоректні координати: {pos}");
            }
        }

        Debug.Log($"Записано {count} Tile-об'єктів у сітку");
    }

    void PlaceAllShips()
    {
        List<ShipDefinition> ships = new List<ShipDefinition>()
        {
            new ShipDefinition(4, 1),
            new ShipDefinition(3, 2),
            new ShipDefinition(2, 3),
            new ShipDefinition(1, 4),
        };

        foreach (ShipDefinition ship in ships)
        {
            for (int i = 0; i < ship.count; i++)
            {
                PlaceShip(ship.length);
            }
        }
    }

    void PlaceShip(int length)
    {
        int attempts = 0;

        while (attempts < 1000)
        {
            bool horizontal = Random.value > 0.5f;
            int x = Random.Range(0, horizontal ? 10 - length + 1 : 10);
            int y = Random.Range(0, horizontal ? 10 : 10 - length + 1);

            List<Vector2Int> candidatePositions = new List<Vector2Int>();

            for (int i = 0; i < length; i++)
            {
                int tx = x + (horizontal ? i : 0);
                int ty = y + (horizontal ? 0 : i);

                if (!IsValidPlacement(tx, ty))
                {
                    candidatePositions.Clear();
                    break;
                }

                candidatePositions.Add(new Vector2Int(tx, ty));
            }

            if (candidatePositions.Count == length)
            {
                Ship vessel = new Ship();

                foreach (var pos in candidatePositions)
                {
                    Tile t = grid[pos.x, pos.y];
                    t.IsOccupied = true;
                    t.LinkedVessel = vessel;
                    vessel.Tiles.Add(t);
                }

                enemyShips.Add(vessel);
                MarkForbidden(candidatePositions);
                return;
            }

            attempts++;
        }

        Debug.LogWarning($"Не вдалося розмістити корабель довжиною {length}");
    }

    bool IsValidPlacement(int x, int y)
    {
        if (x < 0 || x >= 10 || y < 0 || y >= 10) return false;
        if (grid[x, y] == null) return false;
        if (grid[x, y].IsOccupied) return false;
        if (forbidden[x, y]) return false;

        return true;
    }

    void MarkForbidden(List<Vector2Int> shipTiles)
    {
        foreach (Vector2Int pos in shipTiles)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = pos.x + dx;
                    int ny = pos.y + dy;

                    if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10 && grid[nx, ny] != null)
                    {
                        forbidden[nx, ny] = true;
                    }
                }
            }
        }
    }
}
