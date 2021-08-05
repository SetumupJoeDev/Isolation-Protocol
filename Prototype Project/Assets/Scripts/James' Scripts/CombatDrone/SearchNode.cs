using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchNode : MonoBehaviour
{

    public LootDropper m_lootDropper;

    public void GenerateLoot( )
    {
        m_lootDropper.DropLoot( );

        Destroy(gameObject);

    }
}
