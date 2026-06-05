using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] ParticleSystem jetParticle;

    Vector2 move;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        move = input.normalized * speed;

        if (input.y > 0)
        {
            if (!jetParticle.isPlaying) jetParticle.Play();
        }
        else
        {
            if (jetParticle.isPlaying)
            {
                jetParticle.Stop();
                jetParticle.time = 0;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x, rb.velocity.y);
        if (move.y != 0) rb.velocity = new Vector2(rb.velocity.x, move.y);
    }
}
