using Assets.script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy : Bird
{

    public float maxLow = -2;
    public float maxHight = 3;

    public override void Start()
    {
        init();
    }

    public override void init()
    {
        this.updateStatus(BirdStatus.FLY);
        float y = UnityEngine.Random.Range(maxLow, maxHight);
        this.transform.localPosition = new Vector3(this.transform.position.x, y, 0);
    }

    public override void registerFire()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer > (1 / fireSpeed))
        {
            GameObject bullte = Instantiate(buttetTemplate);
            bullte.transform.position = new Vector3(this.transform.position.x, this.transform.position.y);
            bullte.GetComponent<Buttet>().Dir = -1;
            fireTimer = 0f;
        }
    }

    public override void registerMove()
    {
        this.transform.position += new Vector3(-Time.deltaTime * speed,0,0);
    }

    public override void Stop()
    {
        this.status = BirdStatus.DIE;
    }


    public override bool checkDie(Buttet buttet)
    {
        return buttet.side == Side.PLAYER;
    }
}
