using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : MonoBehaviour
{
    Rigidbody2D rb;
    ProceduralBehaviour.DNA dna;

    float gimbleDecision;

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
    }

    private void FixedUpdate()
    {
        rb.angularVelocity += dna.gimble * gimbleDecision;
    }
}
