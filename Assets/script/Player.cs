using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator anim;

    public float force = 100;


    public enum PlayerStatus
    {
        IDLE,
        FLY,
        DIE
    }


    public PlayerStatus status;

    // Start is called before the first frame update
    void Start()
    {
        this.updateStatus(PlayerStatus.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status == PlayerStatus.IDLE)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)){
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force));
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("GameObject1 collided with " + col.name);
        this.updateStatus(PlayerStatus.IDLE);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D Collision with " + col.collider.name);
        this.updateStatus(PlayerStatus.IDLE);
    }

    public void updateStatus(PlayerStatus status)
    {
        this.status = status;
        switch (status)
        {
            case PlayerStatus.IDLE:
                this.rigidbodyBird.Sleep();
                this.anim.SetTrigger("Idle");
                break;

            case PlayerStatus.FLY:
                this.rigidbodyBird.WakeUp();
                this.anim.SetTrigger("Fly");
                break;

            case PlayerStatus.DIE:
                this.anim.SetTrigger("Die");
                break;

            default:
                this.anim.SetTrigger("Idle");
                break;
        }

    }
}
