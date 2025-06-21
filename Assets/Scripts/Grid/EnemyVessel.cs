/*using System.Collections.Generic;
using UnityEngine;

public class EnemyVessel
{
    public List<Tile> Tiles = new List<Tile>();

    public bool IsSunk()
    {
        foreach (var tile in Tiles)
        {
            if (!tile.IsHit)
                return false;
        }
        return true;
    }

    public void HighlightAll()
    {
        foreach (var tile in Tiles)
        {
            tile.Highlight();
        }
    }

    public void MarkAsDestroyed()
    {
        foreach (var tile in Tiles)
        {
            tile.SetMaterial(tile.hitMaterial);
        }
    }
}
*/