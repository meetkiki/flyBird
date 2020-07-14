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

    public delegate void BirdDeath();

    public event BirdDeath onDeath;

    public UnityAction<int> onScore;

    public GameObject buttetTemplate;

    public Vector3 initVector;

    public enum BirdStatus
    {
        IDLE,
        FLY,
        DIE
    }

    public BirdStatus status;

    // Start is called before the first frame update
    public void Start()
    {
        this.updateStatus(BirdStatus.FLY);
        this.initVector = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    public void Update()
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

    float fireTimer = 0f;

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
