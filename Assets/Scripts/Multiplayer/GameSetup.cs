using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviourPun
{
    public PlayerGridGenerator playerGridGenerator;
    public EnemyGridGenerator enemyGridGenerator;

    public Camera playerCamera;
    public Camera enemyCamera;

    void Start()
    {
        if (playerCamera == null || enemyCamera == null)
        {
            Debug.LogError("❌ Камери не призначено в інспекторі");
            return;
        }

        Debug.Log(photonView.IsMine ? "🎮 Я — гравець (host)" : "🤖 Я — другий гравець");

        // 🎯 Усі гравці мають мати доступ до свого поля та до сітки ворога
        playerGridGenerator.gameObject.SetActive(true);
        enemyGridGenerator.gameObject.SetActive(true);

        if (photonView.IsMine)
        {
            // 👤 Для себе — генеруємо кораблі
            playerGridGenerator.GenerateShips();

            // 🔲 Поле ворога — порожня сітка для кліків
            enemyGridGenerator.GenerateGrid();

            playerCamera.gameObject.SetActive(true);
            enemyCamera.gameObject.SetActive(false);
        }
        else
        {
            // 👤 Для другого гравця — своє поле з кораблями
            playerGridGenerator.GenerateShips();

            // 🔲 Поле хоста — порожня сітка
            enemyGridGenerator.GenerateGrid();

            playerCamera.gameObject.SetActive(false);
            enemyCamera.gameObject.SetActive(true);
        }
    }
}
