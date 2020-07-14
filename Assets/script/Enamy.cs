using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy : Bird
{

    public override void registerFire()
    {
        
    }

    public override void registerMove()
    {
        this.transform.position += new Vector3(-Time.deltaTime * speed,0,0);
    }
}
