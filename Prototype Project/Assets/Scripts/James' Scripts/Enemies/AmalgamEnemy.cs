using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmalgamEnemy : EnemyBase
{

    #region Attacking

    [Header("Attacking")]

    [Tooltip("The duration of this enemy's attack windup.")]
    public float m_windupDuration;

    [Tooltip("The range of this enemy's attack effect.")]
    public float m_aoeRange;

    #endregion

}
