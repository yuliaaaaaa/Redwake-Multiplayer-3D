using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Grid References")]
    [SerializeField] private EnemyGridGenerator enemyGrid;
    [SerializeField] private PlayerGridGenerator playerGrid;

    [Header("Guns")]
    [SerializeField] private GunController playerGun;
    [SerializeField] private GunController enemyGun;

    [SerializeField] private GameOverManager gameOverManager;

    private List<Ship> playerShips;
    private List<Ship> enemyShips;

    private bool isPlayerTurn = true;
    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetShips(List<Ship> player, List<Ship> enemy)
    {
        playerShips = player;
        enemyShips = enemy;
    }

    public void OnPlayerClick(Tile tile)
    {
        if (!isPlayerTurn || tile.IsHit || gameEnded) return;

        playerGun.FireAt(tile.transform.position, tile);
        isPlayerTurn = false;
    }

    public void OnCannonballHitCompleted(Tile tile)
    {
        // Перевірка завершення гри
        if (tile.IsEnemyField && AllShipsSunk(enemyShips))
        {
            EndGame(true);
            return;
        }
        else if (!tile.IsEnemyField && AllShipsSunk(playerShips))
        {
            EndGame(false);
            return;
        }

        // Якщо влучив — той же гравець ходить знову
        if (tile.IsOccupied)
        {
            if (tile.IsEnemyField)
                Debug.Log("Гравець влучив — ще один хід!");
            else
                Debug.Log("Ворог влучив — ще один хід!");

            Invoke(tile.IsEnemyField ? nameof(PlayerTurn) : nameof(EnemyTurn), 0.8f);
            return;
        }

        // Промах — передаємо хід іншому
        if (tile.IsEnemyField)
        {
            Debug.Log("Гравець промахнувся — хід ворога");
            isPlayerTurn = false;
            Invoke(nameof(EnemyTurn), 1f);
        }
        else
        {
            Debug.Log("Ворог промахнувся — хід гравця");
            isPlayerTurn = true;
        }
    }

    void PlayerTurn()
    {
        if (gameEnded) return;
        isPlayerTurn = true;
    }

    void EnemyTurn()
    {
        if (gameEnded) return;

        Vector2Int target = GetRandomUntouchedTileFrom(playerGrid.Grid);
        Tile tile = playerGrid.Grid[target.x, target.y];

        enemyGun.FireAt(tile.transform.position, tile);
    }

    Vector2Int GetRandomUntouchedTileFrom(Tile[,] grid)
    {
        for (int i = 0; i < 1000; i++)
        {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            if (!grid[x, y].IsHit)
                return new Vector2Int(x, y);
        }

        Debug.LogWarning("All cells are already affected");
        return Vector2Int.zero;
    }

    bool AllShipsSunk(List<Ship> ships)
    {
        if (ships == null || ships.Count == 0)
        {
            Debug.LogWarning("No ships to check");
            return false;
        }

        foreach (var ship in ships)
        {
            if (!ship.IsSunk())
            {
                Debug.Log($"Still alive ship with {ship.Tiles.Count} cells");
                return false;
            }
        }

        Debug.Log("All ships destroyed");
        return true;
    }

    void EndGame(bool playerWon)
    {
        if (gameEnded) return;
        gameEnded = true;

        Debug.Log(playerWon ? "You won." : "You lose.");
        Time.timeScale = 0f;

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(playerWon ? "You won." : "You lose.");
        }
        else
        {
            Debug.LogWarning("GameOverManager не призначений");
        }
    }
}
