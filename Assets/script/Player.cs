using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator anim;

    // public float force = 100;
    public float speed = 10f;

    public float fireSpeed = 10f;

    public delegate void PlayerDeath();

    public event PlayerDeath onDeath;

    public UnityAction<int> onScore;

    public GameObject buttetTemplate;

    private Vector3 initVector;

    private static string SCORE_AREA = "ScoreArea";

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
        this.initVector = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status != PlayerStatus.FLY)
        {
            return;
        }

        // 移动
        registerMove();

        // 开火
        registerFire();
    }

    float fireTimer = 0f;

    private void registerFire()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer > (1 / fireSpeed) && Input.GetButton("Fire bullets"))
        {
            GameObject bullte = Instantiate(buttetTemplate);
            bullte.transform.position = new Vector3(this.transform.position.x, this.transform.position.y);

            fireTimer = 0;
        }
    }

    private void registerMove()
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
        if (this.status != PlayerStatus.FLY)
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
        if (this.status != PlayerStatus.FLY)
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
                this.transform.localPosition = this.initVector;
                this.rigidbodyBird.WakeUp();
                this.anim.SetTrigger("Fly");
                break;

            case PlayerStatus.DIE:
                this.anim.SetTrigger("Die");
                this.onDeath?.Invoke();
                break;

            default:
                this.rigidbodyBird.Sleep();
                this.anim.SetTrigger("Idle");
                break;
        }

    }
}
