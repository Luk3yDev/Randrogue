using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;

    public int SIZEX = 128;
    public int SIZEY = 256;

    public Vector2 amplitude;
    public Vector2 frequency;
    public Vector2 threshold;
    

    Tile[] tilePalette;

    private void Awake()
    {
        GenerateMap();
    }

    //float timer = 1f;
    private void Update()
    {
        /*
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            groundTilemap.ClearAllTiles();
            GenerateMap();
            timer = 1f;
        }
        */
    }

    public void GenerateMap()
    {
        tilePalette = new Tile[Random.Range(1, 8)];

        for (int i = 0; i < tilePalette.Length; i++)
        {
            tilePalette[i] = ProceduralTile.CreateTile();
        }

        int seed = Random.Range(-10000, 10000);
        Random.InitState(System.DateTime.Now.Millisecond);

        // get noise values
        float freq = Random.Range(frequency.x, frequency.y);
        float amp = Random.Range(amplitude.x, amplitude.y);

        float thresh = Random.Range(threshold.x, threshold.y);

        for (int x = 0; x < SIZEX; x++)
        {
            for (int y = 0; y < SIZEY; y++)
            {
                Vector3Int tilepos = new Vector3Int(x, y, 0);

                float wimble = Mathf.PerlinNoise((x + seed) * freq, (y + seed) * freq) * amp;
                float glorb = Mathf.PerlinNoise((x - seed) / amp, (y - seed) / amp) / freq;

                float werm = wimble * glorb;

                int tile = (int)(wimble * tilePalette.Length);
                    
                if (wimble > thresh)
                {
                    groundTilemap.SetTile(tilepos, tilePalette[tile]);
                }
            }
        }
    }
}
