/*using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NativeWebSocket;

public class ShipSender : MonoBehaviour
{
    public GridGenerator grid;

    private WebSocket websocket;

    async void Start()
    {
        websocket = new WebSocket("ws://localhost:8765");
        //websocket = new WebSocket("ws://host.docker.internal:8765");

        websocket.OnOpen += async () =>
        {
            Debug.Log("Підключено до Python WebSocket");
            await SendShips();
        };

        websocket.OnError += (e) => Debug.LogError($"WebSocket Error: {e}");
        websocket.OnClose += (e) => Debug.Log("З'єднання закрите");

        await websocket.Connect();
    }

    public async Task SendShips()
    {
        ShipLayout layout = new();
        bool[,] visited = new bool[grid.gridSize, grid.gridSize];

        for (int x = 0; x < grid.gridSize; x++)
        {
            for (int y = 0; y < grid.gridSize; y++)
            {
                Tile tile = grid.Grid[x, y];
                if (tile.IsOccupied && !visited[x, y])
                {
                    ShipPosition ship = new();
                    FloodFill(grid, x, y, visited, ship.positions);
                    layout.ships.Add(ship);
                }
            }
        }

        string json = JsonUtility.ToJson(layout, true);
        Debug.Log("Відправляємо кораблі у Python:\n" + json);

        await websocket.SendText(json);
    }

    void Update()
    {
        websocket?.DispatchMessageQueue();
    }

    void OnApplicationQuit()
    {
        websocket?.Close();
    }

    void FloodFill(GridGenerator grid, int x, int y, bool[,] visited, List<Vector2Int> result)
    {
        if (x < 0 || y < 0 || x >= grid.gridSize || y >= grid.gridSize) return;
        if (visited[x, y]) return;

        Tile tile = grid.Grid[x, y];
        if (!tile.IsOccupied) return;

        visited[x, y] = true;
        result.Add(tile.GridPosition);

        FloodFill(grid, x + 1, y, visited, result);
        FloodFill(grid, x - 1, y, visited, result);
        FloodFill(grid, x, y + 1, visited, result);
        FloodFill(grid, x, y - 1, visited, result);
    }
}

[System.Serializable]
public class ShipPosition
{
    public List<Vector2Int> positions = new();
}

[System.Serializable]
public class ShipLayout
{
    public List<ShipPosition> ships = new();
}*/