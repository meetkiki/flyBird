using Assets.script;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : Bird
{
    private static string SCORE_AREA = "ScoreArea";

    public int areaFrame = 5;

    public float playerHp = 1000;

    public Slider slider;

    public override void init()
    {
        slider.maxValue = playerHp;
        slider.value = playerHp;
    }


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

        Vector3 position = Camera.main.WorldToScreenPoint(this.transform.position);

        checkPosition(ref vertical, ref horizontal, position);

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

    private void checkPosition(ref float vertical, ref float horizontal, Vector3 position)
    {
        if (position.x < (Screen.safeArea.xMin + areaFrame) && horizontal < 0)
        {
            horizontal = 0;
        }
        if (position.x > (Screen.safeArea.xMax - areaFrame) && horizontal > 0)
        {
            horizontal = 0;
        }
        if (position.y < (Screen.safeArea.yMin + areaFrame) && vertical < 0)
        {
            vertical = 0;
        }
        if (position.y > (Screen.safeArea.yMax - 2 * areaFrame) && vertical > 0)
        {
            vertical = 0;
        }
    }

    public override void shot(Buttet buttet)
    {
        if (buttet.side == Side.ENAMY)
        {
            this.updateHp(buttet.buttetHurt);
        }
    }

    private void updateHp(float hurt)
    {
        this.playerHp -= hurt;

        if (this.playerHp <= 0)
        {
            this.updateStatus(BirdStatus.DIE);
        }
    }

    public override void triggerEnter2D(Collider2D col)
    {
        Bird bird = col.gameObject.GetComponent<Bird>();
        if (bird != null && bird.side == Side.ENAMY)
        {
            Debug.Log("OnTriggerEnter2D Collision with " + bird.name);

            this.shotEnamy(bird);
        }
    }

    public void shotEnamy(Bird bird)
    {
        this.updateHp(bird.collisionHurt);
    }

    public override void Update()
    {
        base.Update();
        this.slider.value = Mathf.Lerp(this.slider.value, this.playerHp, Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D Collision with " + col.collider.name);
        //this.updateStatus(PlayerStatus.DIE);
    }
}
