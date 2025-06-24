using UnityEngine;

public class EnemyGridGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 10;
    public float spacing = 1.1f;

    public Material enemyDefaultMaterial;

    private Tile[,] grid = new Tile[10, 10];
    public Tile[,] Grid => grid;

    public void GenerateGrid(ShotSynchronizer synchronizer)
    {
        if (synchronizer == null)
        {
            Debug.LogError("Не передано ShotSynchronizer у GenerateGrid!");
        }

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 pos = transform.position + new Vector3(x * spacing, -y * spacing, 0);
                GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                Tile tile = tileObj.GetComponent<Tile>();
                tile.Init(new Vector2Int(x, y), isEnemy: true);
                tile.SetMaterial(enemyDefaultMaterial);

                grid[x, y] = tile;

                EnemyTileClickHandler clickHandler = tileObj.GetComponent<EnemyTileClickHandler>();
                if (clickHandler != null)
                {
                    clickHandler.SetShotSynchronizer(synchronizer);
                }
                else
                {
                    Debug.LogWarning($"На Tile ({x},{y}) немає EnemyTileClickHandler");
                }
            }
        }
    }
}

