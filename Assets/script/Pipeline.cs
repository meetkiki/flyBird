using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{

    public float speed = 2;
    public float maxLow = -2;
    public float maxHight = 3;
    // Start is called before the first frame update
    void Start()
    {
        float y = Random.Range(maxLow, maxHight);
        this.transform.localPosition += new Vector3(0, y, 0);
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(-speed, 0) * Time.deltaTime;
    }
}
