using Assets.script;
using UnityEngine;

public class Buttet : MonoBehaviour
{

    public float speed = 10f;

    public Side side;

    public float buttetHurt = 10;

    private int dir = 1;



    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Dir * speed * Time.deltaTime,0,0);

        
        bool withinTheScreen = Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position));
        if (!withinTheScreen)
        {
            Destroy(this.gameObject, 0.5f);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Buttet buttet = collision.gameObject.GetComponent<Buttet>();

        if (buttet != null)
        {
            Destroy(buttet.gameObject);
        }
    }


    public int Dir
    {
        get
        {
            return dir;
        }

        set
        {
            dir = value;
        }
    }
}
