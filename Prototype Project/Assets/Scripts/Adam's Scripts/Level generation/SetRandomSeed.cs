using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the seed of the randomizer to be controlled
public class SetRandomSeed : MonoBehaviour
{
    [Tooltip("The seed value")]
    public int seed;

    [Tooltip("Whether or not the seed value is random")]
    public bool randomizeSeed;

    private void Awake()
    {
        if (randomizeSeed)
        {
            seed = Random.Range(0, 99999);
        }

        Random.InitState(seed);
    }
}
