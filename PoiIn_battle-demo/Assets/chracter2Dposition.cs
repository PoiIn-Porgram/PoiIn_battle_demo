using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chracter2Dposition : MonoBehaviour
{
    /// <summary>
    /// 抽象坐标和真实坐标的转换函数
    /// 目前这里给出的是真实坐标对虚拟坐标的转换
    /// </summary>
    /// <param name="_3Dposition"></param>
    /// <returns></returns>
    public static Vector3 get2Dposition(Vector3 _3Dposition)//改了static不知道有没有bug
    {
        return new Vector3((_3Dposition.x-_3Dposition.z)*0.3f,
            (_3Dposition.z+_3Dposition.x)*0.15f,
            _3Dposition.z+_3Dposition.x);
    }
}
