using System.Collections;
using UnityEngine;

public class PipelineManger : MonoBehaviour
{

    public GameObject pipelineTemplate;

    public float delay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Coroutine coroutine;

    public void startRun()
    {

        // 开启一个协程 生成管道
        coroutine = StartCoroutine(GeneratePipelines());
    }

    public void stopRun()
    {
        // 停止协程
        StopCoroutine(coroutine);
    }


    IEnumerator GeneratePipelines()
    {
        while (true)
        {
            GeneratePipeline();

            yield return new WaitForSeconds(delay);
        }
    }


    void GeneratePipeline()
    {
        Instantiate(pipelineTemplate, this.transform);
    }

}
