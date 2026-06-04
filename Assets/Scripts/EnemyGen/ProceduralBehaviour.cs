using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralBehaviour
{
    public struct DNA
    {
        public float gravityScale;
        public float acceleration;

        public PhysicsMaterial2D physMat;

        public float gimble;
    }

    public static DNA NewDNA()
    {
        DNA dna = new DNA();
        dna.gravityScale = Random.Range(-2f, 2f);
        dna.acceleration = Random.Range(0.1f, 5f);

        dna.physMat = new PhysicsMaterial2D();
        dna.physMat.bounciness = Random.Range(0f, 1f);
        dna.physMat.friction = Random.Range(0f, 1f);

        dna.gimble = Random.Range(-10f, 10f);

        return dna;
    }
}
