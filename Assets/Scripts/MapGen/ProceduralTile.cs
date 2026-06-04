using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ProceduralTile
{
    public static Tile CreateTile()
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();

        Texture2D texture = ProceduralTexture.CreateTexture(ProceduralTexture.TextureStyle.MirrorXY, 8, false);

        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        float pixelsPerUnit = 8f;
        tile.sprite = Sprite.Create(texture, rect, pivot, pixelsPerUnit);

        tile.colliderType = Tile.ColliderType.Grid;

        return tile;
    }
}
