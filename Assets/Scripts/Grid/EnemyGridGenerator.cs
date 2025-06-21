using UnityEngine;

public class EnemyGridGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 10;
    public float spacing = 1.1f;

    public Material enemyDefaultMaterial;

    void Start()
    {
        GenerateGrid();

        // 🟢 Генеруємо кораблі ПІСЛЯ створення всіх тайлів
        FindObjectOfType<ShipPlacementManager>()?.GenerateShips();
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
                tile.Init(new Vector2Int(x, y), isEnemy: true);
                tile.SetMaterial(enemyDefaultMaterial);
            }
        }
    }
}
