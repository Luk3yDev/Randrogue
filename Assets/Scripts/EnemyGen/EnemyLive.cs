using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : MonoBehaviour
{
    Rigidbody2D rb;
    ProceduralBehaviour.DNA dna;

    float gimbleDecision;
    Vector2 wiggleDecision;

    void ApplyDNA()
    {
        rb.gravityScale = dna.gravityScale;

        PhysicsMaterial2D physMat = dna.physMat;
        rb.sharedMaterial = physMat;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        dna = ProceduralBehaviour.NewDNA();
        ApplyDNA();
    }

    void Update()
    {
        gimbleDecision = Mathf.PerlinNoise(transform.position.x * transform.rotation.z, 
                                           transform.position.y * transform.rotation.z);

        wiggleDecision = new Vector2(Mathf.PerlinNoise(gimbleDecision * rb.velocity.y, gimbleDecision * rb.velocity.x)*Random.Range(-1f,1f),
                                     Mathf.PerlinNoise(gimbleDecision * rb.velocity.x, gimbleDecision * rb.velocity.y)*Random.Range(-1f,1f));
    }

    private void FixedUpdate()
    {
        rb.angularVelocity += dna.gimble * gimbleDecision;
        rb.velocity += dna.acceleration * wiggleDecision;
    }
}
