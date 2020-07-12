using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManger : MonoBehaviour
{

    public GameObject pipelineTemplate;

    public float delay = 3f;

    private LinkedList<Pipeline> pipelines = new LinkedList<Pipeline>();
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
        if (pipelines.Count != 0)
        {
            foreach (Pipeline pip in pipelines)
            {
                Destroy(pip.gameObject);
            }
            pipelines.Clear();
        }
        
        // 开启一个协程 生成管道
        coroutine = StartCoroutine(GeneratePipelines());
    }

    public void stopRun()
    {
        foreach (Pipeline pip in pipelines)
        {
            pip.Stop();
        }
        // 停止协程
        StopCoroutine(coroutine);
    }


    IEnumerator GeneratePipelines()
    {
        // 初始化
        while (true)
        {
            Debug.Log("GeneratePipelines " + pipelines.ToString());
            if (pipelines.Count < 4)
            {
                pipelines.AddFirst(GeneratePipeline().GetComponent<Pipeline>());             
            }
            else
            {
                // 拿到最后一个 放到初始位置
                LinkedListNode<Pipeline> last = pipelines.Last;
                Pipeline pipeline = last.Value;
                pipelines.RemoveLast();
                pipeline.init();
                pipelines.AddFirst(pipeline);
            }

            // 等待delay 后执行
            yield return new WaitForSeconds(delay);
        }
    }


    GameObject GeneratePipeline()
    {
        return Instantiate(pipelineTemplate, this.transform);
    }

}
