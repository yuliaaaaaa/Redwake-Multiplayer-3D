using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviourPun
{
    public PlayerGridGenerator playerGridGenerator;
    public EnemyGridGenerator enemyGridGenerator;
    public ShotSynchronizer shotSynchronizer;

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

        playerGridGenerator.gameObject.SetActive(true);
        enemyGridGenerator.gameObject.SetActive(true);

        // 1. Завжди генеруємо своє поле з кораблями
        playerGridGenerator.GenerateShips();

        // 2. Завжди генеруємо поле ворога, передаючи ShotSynchronizer
        enemyGridGenerator.GenerateGrid(shotSynchronizer);

        // 3. Камери
        playerCamera.gameObject.SetActive(photonView.IsMine);
        enemyCamera.gameObject.SetActive(!photonView.IsMine);
    }
}
