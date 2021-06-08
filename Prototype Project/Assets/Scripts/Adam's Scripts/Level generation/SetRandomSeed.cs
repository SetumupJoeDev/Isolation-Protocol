using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the seed of the randomizer to be controlled
public class SetRandomSeed : MonoBehaviour
{
    [Tooltip("The seed value")]
    public int  m_seed;

    [Tooltip("Whether or not the seed value is random")]
    public bool m_randomizeSeed;

    private void Awake()
    {
        if (m_randomizeSeed)
        {
            m_seed = Random.Range(0, 99999);
        }

        Random.InitState(m_seed);
    }
}
