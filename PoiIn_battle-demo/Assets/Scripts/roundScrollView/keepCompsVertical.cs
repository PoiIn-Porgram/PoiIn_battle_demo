using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepCompsVertical : MonoBehaviour
{
    //自动读取子集物体并重置其角度，只需赋给大圆即可
    void Start()
    {

    }

    void Update()
    {
        foreach(Transform i in this.gameObject.transform){
            i.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
    }

}
