using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcTexTest : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        RandSprite();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            RandSprite();
        }
    }

    void RandSprite()
    {
        Texture2D texture = ProceduralTexture.CreateTexture();

        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        float pixelsPerUnit = 16f;

        spriteRenderer.sprite = Sprite.Create(texture, rect, pivot, pixelsPerUnit);
    }
}
