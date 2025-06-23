using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour 
{
    public List<Tile> Tiles = new List<Tile>();
    /// Перевіряє, чи всі клітинки корабля вже влучені
    public bool IsSunk()
    {
        foreach (var tile in Tiles)
        {
            if (!tile.IsHit)
                return false;
        }
        return true;
    }
    /// Виділяє всі клітинки корабля (наприклад, при попаданні)
    public void HighlightAll()
    {
        foreach (var tile in Tiles)
        {
            tile.Highlight();
        }
    }
    /// Помічає всі клітинки як знищені (можна змінити матеріал)
    public void MarkAsDestroyed()
    {
        foreach (var tile in Tiles)
        {
            tile.SetMaterial(tile.hitMaterial); // 🔁 можна змінити на destroyedMaterial
        }
    }
}
