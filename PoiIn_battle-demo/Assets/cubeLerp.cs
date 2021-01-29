using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeLerp : MonoBehaviour
{
    public Vector3 target2Dposition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator cubeLerpMove(GameObject gameObj, float lerpFactor)
    {
        gameObj.transform.position = Vector3.Lerp(gameObj.transform.position, target2Dposition, lerpFactor);
        if (Vector3.SqrMagnitude(target2Dposition -  gameObj.transform.position)<0.005f)
        {
            gameObj.transform.position = target2Dposition;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(cubeLerpMove(gameObj, lerpFactor));
        }
        yield return 0;
    }
    public IEnumerator camLerpMove(GameObject gameObj, GameObject cam, Vector3 reletiveDistance, float lerpFactor)
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, gameObj.transform.position + reletiveDistance, lerpFactor);
        cam.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y,-10);
        if (Vector3.SqrMagnitude(cam.transform.position - reletiveDistance - target2Dposition)<0.00005f)
        {
            cam.transform.position = reletiveDistance + target2Dposition;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(camLerpMove(gameObj, cam, reletiveDistance, lerpFactor));
        }
        yield return 0;
    }
    //打算封装lerp函数但bug频出，target2Dposition不能设成参数，只能作公共变量不然必偏移 //踩过的坑一定要记下来
    //此脚本暂作遗弃处理
    //2.0
    //发现人物移动的lerp函数不能给到每个砖块的controller，而必须单独封装
    //于是此脚本又用上了
    //总结：target2Dposition必须外置，lerp序列对一个对象只能存在一个。

}
