using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EnemyGridGenerator enemyGrid;
    public PlayerGridGenerator playerGrid;

    public GunController playerGun;
    public GunController enemyGun;

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

    public bool IsPlayerTurn() => isPlayerTurn;

    public void OnCannonballHitCompleted(Tile tile)
    {
        if (gameEnded) return;

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

        // ВЛУЧЕННЯ
        if (tile.IsOccupied)
        {
            Debug.Log(tile.IsEnemyField ? "Гравець влучив — ще один хід!" : "Ворог влучив — ще один хід!");
            isPlayerTurn = tile.IsEnemyField;
            return;
        }

        // ПРОМАХ
        if (tile.IsEnemyField)
        {
            Debug.Log("Гравець промахнувся — хід ворога");
            isPlayerTurn = false;
        }
        else
        {
            Debug.Log("Ворог промахнувся — хід гравця");
            isPlayerTurn = true;
        }
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
                return false;
            }
        }

        return true;
    }
    void EndGame(bool playerWon)
    {
        if (gameEnded) return;
        gameEnded = true;

        Time.timeScale = 0f;

        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(playerWon ? "You won." : "You lose.");
        }
    }
}