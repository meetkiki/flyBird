using System;
using System.Collections.Generic;
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

    private AudioSource audiosource;

    private Dictionary<string, AudioClip> audios = new Dictionary<string, AudioClip>();

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
        this.audiosource = this.GetComponent<AudioSource>();
        this.initVector = this.gameObject.transform.localPosition;
        this.loadAudioClips();
    }


    private void loadAudioClips()
    {
        audios.Add("die", (AudioClip)Resources.Load("audios/sfx_die"));
        audios.Add("hit", (AudioClip)Resources.Load("audios/sfx_hit"));
        audios.Add("point", (AudioClip)Resources.Load("audios/sfx_point"));
        audios.Add("swooshing", (AudioClip)Resources.Load("audios/sfx_swooshing"));
        audios.Add("wing", (AudioClip)Resources.Load("audios/sfx_wing"));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.status != PlayerStatus.FLY)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.playAudio("wing");
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force));
            this.transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else
        {
            this.transform.Rotate(0,0,-1);
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

        this.playAudio("hit");
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
            this.playAudio("point");
            return;
        }

       
        this.updateStatus(PlayerStatus.DIE);
    }


    public void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("OnCollisionEnter2D Collision with " + col.collider.name);
        this.updateStatus(PlayerStatus.DIE);
    }


    public void playAudio(string audioName)
    {
        if (audiosource.isPlaying)
        {
            audiosource.Stop();
        }

        AudioClip clip = audios[audioName];
        if (clip != null)
        {
            audiosource.clip = clip;
            audiosource.Play();
        }
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
