using UnityEngine;

public class EnemyTileClickHandler : MonoBehaviour
{
    private ShotSynchronizer shotSynchronizer;
    private Tile tile;

    public void SetShotSynchronizer(ShotSynchronizer synchronizer)
    {
        if (synchronizer == null)
        {
            Debug.LogError("❌ [ClickHandler] ShotSynchronizer не передано!");
            return;
        }

        shotSynchronizer = synchronizer;
    }

    private void Awake()
    {
        tile = GetComponent<Tile>();
        if (tile == null)
        {
            Debug.LogError("❌ [ClickHandler] Не знайдено Tile на обʼєкті!");
        }
    }

    private void OnMouseDown()
    {
        if (tile == null || shotSynchronizer == null) return;

        if (!tile.IsEnemyField)
        {
            Debug.Log($"⛔ Клік по своєму полі: {tile.GridPosition}");
            return;
        }

        if (!GameManager.Instance.IsPlayerTurn())
        {
            Debug.Log("⏳ Зачекай свого ходу!");
            return;
        }

        if (tile.IsHit)
        {
            Debug.Log("🔁 Вже стріляли по цій клітинці");
            return;
        }
        shotSynchronizer.SendFireAt(tile.GridPosition);
    }
}
