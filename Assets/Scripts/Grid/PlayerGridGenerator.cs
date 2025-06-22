using System.Collections.Generic;
using UnityEngine;

public class PlayerGridGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 10;
    public float spacing = 1.1f;
    public Material playerDefaultMaterial;
    public Material occupiedMaterial;

    private Tile[,] grid = new Tile[10, 10];
    private bool[,] forbidden = new bool[10, 10];
    public List<Ship> Ships { get; private set; } = new List<Ship>();

    public Tile[,] Grid => grid;
    void Start()
    {
        GenerateGrid();
        PlaceAllShips();

        ShipPlacementManager shipPlacement = FindObjectOfType<ShipPlacementManager>();
        if (shipPlacement != null && GameManager.Instance != null)
        {
            GameManager.Instance.SetShips(Ships, shipPlacement.Ships);
        }
        else
        {
            Debug.LogWarning("⚠️ Не знайдено ShipPlacementManager або GameManager.Instance — не передано кораблі.");
        }
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 pos = transform.position + new Vector3(x * spacing, -y * spacing, 0);
                GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                Tile tile = tileObj.GetComponent<Tile>();
                tile.Init(new Vector2Int(x, y), false);
                tile.SetMaterial(playerDefaultMaterial);

                grid[x, y] = tile;
            }
        }
    }

    void ResetField()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                forbidden[x, y] = false;
                if (grid[x, y] != null)
                {
                    grid[x, y].IsOccupied = false;
                    grid[x, y].SetMaterial(playerDefaultMaterial);
                    grid[x, y].LinkedVessel = null;
                    grid[x, y].IsHit = false;
                }
            }
        }

        Ships.Clear();
    }

    void PlaceAllShips()
    {
        int maxRetries = 20;

        for (int attempt = 0; attempt < maxRetries; attempt++)
        {
            ResetField();

            int placed4 = PlaceShip(4, 1);
            int placed3 = PlaceShip(3, 2);
            int placed2 = PlaceShip(2, 3);
            int placed1 = PlaceShip(1, 4);

            if (placed4 == 1 && placed3 == 2 && placed2 == 3 && placed1 == 4)
            {
                Debug.Log($"✅ Кораблі гравця розміщено успішно з {attempt + 1}-ї спроби");
                return;
            }
        }

        Debug.LogError("❌ Не вдалося розмістити всі кораблі гравця навіть після кількох спроб.");
    }

    int PlaceShip(int length, int count)
    {
        int placed = 0;
        int maxAttempts = 1000;

        while (placed < count && maxAttempts-- > 0)
        {
            bool horizontal = Random.value > 0.5f;
            int x = Random.Range(0, horizontal ? gridSize - length + 1 : gridSize);
            int y = Random.Range(0, horizontal ? gridSize : gridSize - length + 1);

            bool canPlace = true;

            for (int i = 0; i < length; i++)
            {
                int cx = x + (horizontal ? i : 0);
                int cy = y + (horizontal ? 0 : i);

                if (forbidden[cx, cy])
                {
                    canPlace = false;
                    break;
                }
            }

            if (!canPlace) continue;

            Ship ship = new Ship();

            for (int i = 0; i < length; i++)
            {
                int cx = x + (horizontal ? i : 0);
                int cy = y + (horizontal ? 0 : i);

                Tile tile = grid[cx, cy];
                tile.IsOccupied = true;
                tile.LinkedVessel = ship;
                tile.SetMaterial(occupiedMaterial);

                ship.Tiles.Add(tile);

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int fx = cx + dx;
                        int fy = cy + dy;
                        if (fx >= 0 && fx < gridSize && fy >= 0 && fy < gridSize)
                        {
                            forbidden[fx, fy] = true;
                        }
                    }
                }
            }

            Ships.Add(ship);
            placed++;
        }

        return placed;
    }
}
