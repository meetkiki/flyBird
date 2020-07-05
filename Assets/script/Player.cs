using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator anim;

    public float force = 100;

    public delegate void PlayerDeath();

    public event PlayerDeath onDeath;

    public UnityAction<int> onScore;


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

        if (Input.GetMouseButtonDown(0)){
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force));
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
        
        this.updateStatus(PlayerStatus.DIE);
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

       
        this.updateStatus(PlayerStatus.DIE);
    }


    public void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D Collision with " + col.collider.name);
        this.updateStatus(PlayerStatus.DIE);
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
