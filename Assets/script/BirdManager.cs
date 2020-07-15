using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public GameObject template;

    public float delay = 3f;

    public int maxCount = 3;

    private LinkedList<Bird> birds = new LinkedList<Bird>();

    Coroutine coroutine;

    public void startRun()
    {
        if (birds.Count != 0)
        {
            foreach (Bird bird in birds)
            {
                Destroy(bird.gameObject);
            }
            birds.Clear();
        }

        // 开启一个协程 生成管道
        coroutine = StartCoroutine(GenerateBirds());
    }

    public void stopRun()
    {
        foreach (Bird brid in birds)
        {
            brid.Stop();
        }
        // 停止协程
        StopCoroutine(coroutine);
    }


    IEnumerator GenerateBirds()
    {
        // 初始化
        while (true)
        {
            if (birds.Count <= maxCount)
            {
                birds.AddFirst(GenerateBird().GetComponent<Bird>());
            }
            else
            {
                // 拿到最后一个 放到初始位置
                LinkedListNode<Bird> last = birds.Last;
                Bird bird = last.Value;
                bird.init();
                birds.RemoveLast();
                birds.AddFirst(bird);
            }

            // 等待delay 后执行
            yield return new WaitForSeconds(delay);
        }
    }


    GameObject GenerateBird()
    {
        return Instantiate(template);
    }
}
