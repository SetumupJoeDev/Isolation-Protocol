using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNode : MonoBehaviour
{

    [Tooltip("The loot generator script attached to this object.")]
    public LootDropper m_lootDropper;

    public void GenerateLoot( )
    {

        //Generates loot to drop before being destroyed
        m_lootDropper.DropLoot( );

        //Is destroyed so it can only generate loot once
        Destroy(gameObject);

    }
}
