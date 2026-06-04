using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralTexture
{
    public enum TextureStyle
    {
        Default = 0,
        MirrorX = 1,
        MirrorY = 2,
        MirrorXY = 3
    }

    public static Color32[] CreatePalette(int numColors, bool dedicateAlpha)
    {
        Color32[] palette = new Color32[numColors];
        
        for (int i = 0; i < numColors; i++)
        {
            Color32 color = new Color32((byte)Random.Range(0, 255), // r
                                        (byte)Random.Range(0, 255), // g
                                        (byte)Random.Range(0, 255), // b
                                        255); // a
            palette[i] = color;
        }
        if (dedicateAlpha) palette[0] = new Color32(0, 0, 0, 0);

        return palette;
    }

    public static Texture2D CreateTexture(TextureStyle style, int size, bool dedicateAlpha)
    {
        Color32[] palette = CreatePalette(4, dedicateAlpha);

        Texture2D texture = new Texture2D(size, size);
        if (style == TextureStyle.MirrorX) { texture = MirrorXTexture(texture, palette); }
        else if (style == TextureStyle.MirrorY) { texture = MirrorYTexture(texture, palette); }
        else if (style == TextureStyle.MirrorXY) { texture = MirrorXYTexture(texture, palette); }
        else texture = DefaultTexture(texture, palette);

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    private static Texture2D DefaultTexture(Texture2D texture, Color32[] palette)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Color32 color = palette[Random.Range(0, palette.Length)];
                texture.SetPixel(x, y, color);
            }
        }
        return texture;
    }
    private static Texture2D MirrorXTexture(Texture2D texture, Color32[] palette)
    {
        for (int x = 0; x < texture.width / 2; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Color32 color = palette[Random.Range(0, palette.Length)];
                texture.SetPixel(x, y, color);
                texture.SetPixel(texture.width - x - 1, y, color);
            }
        }
        return texture;
    }
    private static Texture2D MirrorYTexture(Texture2D texture, Color32[] palette)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height / 2; y++)
            {
                Color32 color = palette[Random.Range(0, palette.Length)];
                texture.SetPixel(x, y, color);
                texture.SetPixel(x, texture.height - y - 1, color);
            }
        }
        return texture;
    }
    private static Texture2D MirrorXYTexture(Texture2D texture, Color32[] palette)
    {
        for (int x = 0; x < texture.width / 2; x++)
        {
            for (int y = 0; y < texture.height / 2; y++)
            {
                Color32 color = palette[Random.Range(0, palette.Length)];
                texture.SetPixel(x, y, color);
                texture.SetPixel(texture.width - x - 1, y, color);
                texture.SetPixel(x, texture.height - y - 1, color);
                texture.SetPixel(texture.width - x - 1, texture.height - y - 1, color);
            }
        }
        return texture;
    }
}
