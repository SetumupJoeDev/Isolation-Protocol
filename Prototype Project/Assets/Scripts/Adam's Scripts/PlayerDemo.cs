using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemo : CharacterBase
{

    // Update is called once per frame
    protected override void Update()
    {
        m_directionalVelocity.x = Input.GetAxisRaw("Horizontal");

        m_directionalVelocity.y = Input.GetAxisRaw("Vertical");
    }
}
