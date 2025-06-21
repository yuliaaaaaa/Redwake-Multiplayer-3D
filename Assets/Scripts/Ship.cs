using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public List<Tile> Tiles = new List<Tile>();

    /// <summary>
    /// Перевіряє, чи всі клітинки корабля вже влучені
    /// </summary>
    public bool IsSunk()
    {
        foreach (var tile in Tiles)
        {
            if (!tile.IsHit)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Виділяє всі клітинки корабля (наприклад, при попаданні)
    /// </summary>
    public void HighlightAll()
    {
        foreach (var tile in Tiles)
        {
            tile.Highlight();
        }
    }

    /// <summary>
    /// Помічає всі клітинки як знищені (можна змінити матеріал)
    /// </summary>
    public void MarkAsDestroyed()
    {
        foreach (var tile in Tiles)
        {
            tile.SetMaterial(tile.hitMaterial); // 🔁 можна змінити на destroyedMaterial
        }
    }
}
