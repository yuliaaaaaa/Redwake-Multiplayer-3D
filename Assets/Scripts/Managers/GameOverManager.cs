using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerGridGenerator playerGrid;
    [SerializeField] private EnemyGridGenerator enemyGrid;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;


    private bool gameEnded = false;

    public void CheckGameOver()
    {
        if (gameEnded) return;

        if (AreAllShipsSunk(enemyGrid.Grid))
        {
            ShowGameOver("🎉 Ти переміг!");
        }
        else if (AreAllShipsSunk(playerGrid.Grid))
        {
            ShowGameOver("💀 Поразка");
        }
    }

    private bool AreAllShipsSunk(Tile[,] grid)
    {
        HashSet<Ship> counted = new HashSet<Ship>();

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Tile tile = grid[x, y];
                Ship ship = tile.LinkedVessel;

                if (ship != null && !counted.Contains(ship))
                {
                    counted.Add(ship);
                    if (!ship.IsSunk())
                        return false;
                }
            }
        }

        return true;
    }

    public void ShowGameOver(string message)
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (gameOverText != null)
            gameOverText.text = message;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log($"🏁 Гра завершена: {message}");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
