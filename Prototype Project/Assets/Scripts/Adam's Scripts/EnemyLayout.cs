using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLayout", menuName = "ScriptableObjects/Enemies/EnemyLayouts", order = 1)]
public class EnemyLayout : ScriptableObject
{
    public GameObject[] m_enemies;

    public Vector3[] m_localPositions;
}
