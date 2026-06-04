using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralTexture
{
    public static Color32[] CreatePalette(int numColors)
    {
        Color32[] palette = new Color32[numColors];
        palette[0] = new Color32(0, 0, 0, 0);
        for (int i = 1; i < numColors; i++)
        {
            Color32 color = new Color32((byte)Random.Range(0, 255), // r
                                        (byte)Random.Range(0, 255), // g
                                        (byte)Random.Range(0, 255), // b
                                        255); // a
            palette[i] = color;
        }
        return palette;
    }

    public static Texture2D CreateTexture()
    {
        Color32[] palette = CreatePalette(4);
        Debug.Log($"Color palette sample: {palette[0].r},{palette[0].g},{palette[0].b}   {palette[1].r},{palette[1].g},{palette[1].b}");

        Texture2D texture = new Texture2D(16, 16); // size x y
        for (int x = 0; x < texture.width/2; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Color32 color = palette[Random.Range(0, palette.Length)];
                texture.SetPixel(x, y, color);
                texture.SetPixel(texture.width-x-1, y, color);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
}
