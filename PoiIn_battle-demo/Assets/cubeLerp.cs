using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeLerp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator lerpMove(GameObject gameObj, Vector3 target2Dposition, float lerpFactor)
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
            StartCoroutine(lerpMove(gameObj, target2Dposition, lerpFactor));
        }
        yield return 0;
    }
    //打算封装lerp函数但bug频出，target2Dposition不能设成参数，只能作公共变量不然必偏移 //踩过的坑一定要记下来
    //此脚本暂作遗弃处理

}
