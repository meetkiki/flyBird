using UnityEngine;

public class Pipeline : MonoBehaviour
{

    public float speed = 2;
    public float maxLow = -2;
    public float maxHight = 3;

    private bool running;
    // Start is called before the first frame update
    void Start()
    {
        init(); 
    }
    
    public void init()
    {
        this.init(0);
    }

    public void init(int x)
    {
        float y = Random.Range(maxLow, maxHight);
        this.transform.localPosition = new Vector3(x, y, 0);
        this.running = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (this.running)
        {
            this.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
        }
    }

    public void Stop()
    {
        this.running = false;
    }
}
