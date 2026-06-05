using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Tilemap groundTilemap;

    public int SIZEX = 128;
    public int SIZEY = 256;

    [Header("Cellular")]
    public int iterations = 20;

    [Header("Perlin")]
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

    int GetNeighbours(int[,] mapData, int x, int y, int me)
    {
        if (x == 0 ||
            x == SIZEX-1 ||
            y == 0 ||
            y == SIZEY-1)
        {
            return 0;
        }

        int n = 0;
        if (mapData[x+1,y  ] == me) { n++; }
        if (mapData[x  ,y+1] == me) { n++; }
        if (mapData[x+1,y+1] == me) { n++; }
        if (mapData[x-1,y  ] == me) { n++; }
        if (mapData[x  ,y-1] == me) { n++; }
        if (mapData[x-1,y-1] == me) { n++; }
        if (mapData[x+1,y-1] == me) { n++; }
        if (mapData[x-1,y+1] == me) { n++; }
        return n;
    }

    public void GenerateMap()
    {
        tilePalette = new Tile[Random.Range(2, 8)];

        for (int i = 0; i < tilePalette.Length; i++)
        {
            tilePalette[i] = ProceduralTile.CreateTile();
        }

        //PerlinMethod();

        int[,] mapData = new int[SIZEX,SIZEY];

        int love = Random.Range(1,8); // cell rules
        int hate = Random.Range(1,8);

        for (int x = 0; x < SIZEX; x++) // 1. NOISE
        {
            for (int y = 0; y < SIZEY; y++)
            {
                mapData[x,y] = Random.Range(0, tilePalette.Length+1);
            }
        }
        int[,] buffer = mapData; // create buffer to remove bias
        for (int i = 0; i < iterations; i++) // 2. APPLY RULES
        {
            if (Random.Range(0, 5) == 0)
            {
                love = Random.Range(1,8);
                hate = Random.Range(1,8); // reroll rules sometimes
            }

            for (int x = 0; x < SIZEX; x++)
            {
                for (int y = 0; y < SIZEY; y++)
                {
                    int me = mapData[x,y];
                    int n = GetNeighbours(mapData, x, y, me);

                    if (me != 0)
                    {
                        if (n > hate && n < love) buffer[x,y] = 0;
                    }
                    else
                    {
                        if (n < hate && n > love) buffer[x,y] = me;
                    }
                }
            }
        }
        mapData = buffer; // swap buffers
        for (int x = 0; x < SIZEX; x++) // 3. TILEMAP
        {
            for (int y = 0; y < SIZEY; y++)
            {
                Vector3Int tilepos = new Vector3Int(x, y, 0);

                if (mapData[x,y] != 0)
                    groundTilemap.SetTile(tilepos, tilePalette[mapData[x,y]-1]);
            }
        }
    }

    void PerlinMethod()
    {
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

                int tile = (int)((wimble + (2 * thresh)) * tilePalette.Length);
                    
                if (wimble > thresh)
                {
                    groundTilemap.SetTile(tilepos, tilePalette[tile]);
                }
            }
        }
    }
}
