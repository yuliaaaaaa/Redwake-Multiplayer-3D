using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }
    public bool IsOccupied { get; set; } = false;
    public bool IsHit { get; set; } = false;
    public bool IsEnemyField { get; set; } = false;

    public Material hitMaterial;
    public Material missMaterial;
    public Material highlightMaterial;
    public Ship LinkedVessel { get; set; }

    private Renderer _renderer;
    private Material _defaultMaterial;

    public void Init(Vector2Int pos, bool isEnemy)
    {
        GridPosition = pos;
        IsEnemyField = isEnemy;
        _renderer = GetComponent<Renderer>();
    }

    public void SetMaterial(Material mat)
    {
        _renderer.sharedMaterial = mat;
        _defaultMaterial = mat;
    }

    public void Highlight()
    {
        if (highlightMaterial != null)
            _renderer.sharedMaterial = highlightMaterial;
    }

    public void ResetColor()
    {
        if (_defaultMaterial != null)
            _renderer.sharedMaterial = _defaultMaterial;
    }

    public void OnHit()
    {
        if (IsHit) return;
        IsHit = true;

        if (IsOccupied)
        {
            Debug.Log($"💥 ВЛУЧЕННЯ: Корабель супротивника на клітинці {GridPosition.x}, {GridPosition.y} уражено!");

            if (LinkedVessel != null)
            {
                if (LinkedVessel.IsSunk())
                {
                    Debug.Log("☠️ ВЕСЬ ВОРОЖИЙ СЕКТОР ЗНИЩЕНО");
                    LinkedVessel.MarkAsDestroyed();
                }
                else
                {
                    Highlight();
                }
            }
        }
        else
        {
            Debug.Log($"🌊 ПРОМАХ: На клітинці {GridPosition.x}, {GridPosition.y} немає корабля.");
            _renderer.sharedMaterial = missMaterial;
        }
    }
}
