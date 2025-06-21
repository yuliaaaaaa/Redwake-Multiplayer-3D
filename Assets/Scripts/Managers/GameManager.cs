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


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Викликається, коли гравець клікає по клітинці ворога
    public void OnPlayerClick(Tile tile)
    {
        if (!isPlayerTurn || tile.IsHit) return;

        playerGun.FireAt(tile.transform.position, tile);
        isPlayerTurn = false;
    }
    public void SetShips(List<Ship> player, List<Ship> enemy)
    {
        playerShips = player;
        enemyShips = enemy;
    }
    public void OnCannonballHitCompleted(Tile tile)
    {
        // 🏁 Перевірка завершення гри
        if (tile.IsEnemyField && AllShipsSunk(enemyShips))
        {
            Debug.Log("🎉 Гравець переміг!");
            gameOverManager.ShowGameOver("🎉 Гравець переміг!");
            return;
        }
        else if (!tile.IsEnemyField && AllShipsSunk(playerShips))
        {
            Debug.Log("💀 Ворог переміг!");
            EndGame(false);
            return;
        }

        if (tile.IsOccupied)
        {
            if (tile.IsEnemyField)
            {
                Debug.Log("🎯 Гравець влучив — ще один хід!");
            }
            else
            {
                Debug.Log("🎯 Ворог влучив — ще один хід!");
            }

            Invoke(tile.IsEnemyField ? nameof(PlayerTurn) : nameof(EnemyTurn), 0.8f);
            return;
        }

        if (tile.IsEnemyField)
        {
            Debug.Log("🌊 Гравець промахнувся — хід ворога");
            Invoke(nameof(EnemyTurn), 1f);
            isPlayerTurn = false;
        }
        else
        {
            Debug.Log("🌊 Ворог промахнувся — хід гравця");
            isPlayerTurn = true;
        }
    }
    void EnemyTurn()
    {
        Vector2Int target = GetRandomUntouchedTileFrom(playerGrid.Grid);
        Tile tile = playerGrid.Grid[target.x, target.y];

        enemyGun.FireAt(tile.transform.position, tile);
    }

    void PlayerTurn()
    {
        isPlayerTurn = true;
    }

    // Знаходить випадкову невражену клітинку
    Vector2Int GetRandomUntouchedTileFrom(Tile[,] grid)
    {
        for (int i = 0; i < 1000; i++)
        {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            if (!grid[x, y].IsHit)
                return new Vector2Int(x, y);
        }

        Debug.LogWarning("⚠️ Всі клітинки вже вражені");
        return Vector2Int.zero;
    }

    bool AllShipsSunk(List<Ship> ships)
    {
        foreach (var ship in ships)
        {
            if (!ship.IsSunk())
            {
                Debug.Log($"🚢 Ще живий корабель із {ship.Tiles.Count} клітинками");
                return false;
            }
        }

        Debug.Log("✅ Всі кораблі знищено!");
        return true;
    }
    void EndGame(bool playerWon)
    {
        Debug.Log(playerWon ? "🎉 Гравець переміг!" : "💀 Ворог переміг!");
        Time.timeScale = 0f;

        // TODO: показати UI (перемога/поразка), кнопки, тощо
    }

}
