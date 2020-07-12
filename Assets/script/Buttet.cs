using UnityEngine;

public class Buttet : MonoBehaviour
{

    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * Time.deltaTime,0,0);

        
        bool withinTheScreen = Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position));
        if (!withinTheScreen)
        {
            Destroy(this.gameObject, 0.5f);
        }

    }
}
