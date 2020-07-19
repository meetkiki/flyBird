﻿using System;
using Assets.script;
using UnityEngine;
using UnityEngine.Events;
using static Player;

public abstract class Bird : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator anim;

    // public float force = 100;
    public float speed = 10f;

    public float fireSpeed = 10f;

    public float collisionHurt = 10;

    public delegate void BirdDeath(Bird brid);

    public event BirdDeath onDeath;

    public UnityAction<int> onScore;

    public GameObject buttetTemplate;

    public Vector3 initVector;

    public Side side;

    public enum BirdStatus
    {
        IDLE,
        FLY,
        DIE
    }

    public BirdStatus status;

    // Start is called before the first frame update
    public virtual void Start()
    {
        this.init();
        this.updateStatus(BirdStatus.IDLE);
        this.initVector = this.gameObject.transform.localPosition;
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (this.status != BirdStatus.FLY)
        {
            return;
        }

        // 移动
        registerMove();

        // 开火
        registerFire();
    }

    protected float fireTimer = 0f;

    public abstract void registerFire();


    public abstract void registerMove();
    

    public void updateStatus(BirdStatus status)
    {
        this.status = status;
        switch (status)
        {
            case BirdStatus.IDLE:
                this.rigidbodyBird.Sleep();
                this.anim.SetTrigger("Idle");
                break;

            case BirdStatus.FLY:
                this.transform.localPosition = this.initVector;
                this.rigidbodyBird.WakeUp();
                this.anim.SetTrigger("Fly");
                break;

            case BirdStatus.DIE:
                this.die();
                break;

            default:
                this.rigidbodyBird.Sleep();
                this.anim.SetTrigger("Idle");
                break;
        }
    }

    public virtual void die()
    {
        this.anim.SetTrigger("Die");
        this.onDeath?.Invoke(this);
    }

    public virtual void init()
    {
        if (this.initVector != null)
        {
            this.gameObject.transform.localPosition = this.initVector;
        }
    }

    public virtual void Stop()
    {
        this.updateStatus(BirdStatus.DIE);
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (this.status != BirdStatus.FLY)
        {
            return;
        }

        Buttet buttet = col.gameObject.GetComponent<Buttet>();
        if (buttet != null)
        {
            // Debug.Log("OnTriggerEnter2D Collision with " + col.name);

            this.shot(buttet);
        }

        this.triggerEnter2D(col);
    }

    public virtual void triggerEnter2D(Collider2D col)
    {

    }

    public virtual void shot(Buttet buttet)
    {
        if (checkShot(buttet))
        {
            this.updateStatus(BirdStatus.DIE);
        }
    }

    public virtual bool checkShot(Buttet buttet)
    {
        return buttet.side == Side.ENAMY;
    }
}
