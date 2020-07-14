using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : Bird
{
    private static string SCORE_AREA = "ScoreArea";

    float fireTimer = 0f;

    public override void registerFire()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer > (1 / fireSpeed) && Input.GetButton("Fire bullets"))
        {
            GameObject bullte = Instantiate(buttetTemplate);
            bullte.transform.position = new Vector3(this.transform.position.x, this.transform.position.y);

            fireTimer = 0f;
        }
    }

    public override void registerMove()
    {
        // 纵向
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        // 横向
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        this.transform.position += new Vector3(horizontal, vertical, 0);

        if (vertical == 0 || horizontal != 0)
        {
            // 角度清0
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            return;
        }

        float elevation = 10;

        if (vertical > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, elevation);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, -elevation);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (this.status != BirdStatus.FLY)
        {
            return;
        }
        
        //Debug.Log("GameObject1 TriggerEnter with " + col.name);
        if (col.name.Equals(SCORE_AREA))
        {
            return;
        }
        
        //this.updateStatus(PlayerStatus.DIE);
    }


    public void OnTriggerExit2D(Collider2D col)
    {
        if (this.status != BirdStatus.FLY)
        {
            return;
        }
        //Debug.Log("GameObject1 TriggerExit with " + col.name);
        if (col.name.Equals(SCORE_AREA))
        {
            this.onScore?.Invoke(1);
            return;
        }

        //this.updateStatus(PlayerStatus.DIE);
    }


    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D Collision with " + col.collider.name);
        //this.updateStatus(PlayerStatus.DIE);
    }
}
