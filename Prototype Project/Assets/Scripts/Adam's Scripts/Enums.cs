using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class to hold any enums that need to be acessed by multiple scripts
public class Enums : MonoBehaviour
{
    // Possible door directions in rooms for level generation
    public enum Directions { Top, Left, Right, Bottom};

    public enum EnemyTypes { BasicMelee, BasicRanged, FaceHugger, Large, SporeBomber, WallGrowth, ParasiteEgg, Droid};
}
