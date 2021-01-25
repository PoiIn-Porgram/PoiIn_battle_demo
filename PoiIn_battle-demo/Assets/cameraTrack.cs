using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using UnityEngine;

public class cameraTrack : MonoBehaviour
{
    public Vector3 cameraReletivePosition;
    private  GameObject[] _objects2D;
    private Transform chracterTrans;
    /// <summary>
    /// 用于控制摄像机移动
    /// </summary>
    private void Start()
    {
        chracterTrans = FindObjectOfType<chracterMove>().GetComponentInParent<Transform>();
        _objects2D = GameObject.FindGameObjectsWithTag("2D_OBJ");

    }
    //避免使用全局变量，尽量以纯函数的方式进行开发
    //类—对象模式
    //数据和引擎分离，避免对数据硬编码
    //构造函数，通过集成共享数据
    
    public void RotateTheCamera(int revolveDirection)
    {
        cameraReletivePosition = Quaternion.Euler(0, 90, 0)*cameraReletivePosition;
        this.transform.rotation = Quaternion.Euler(0, 90, 0) * this.transform.rotation;
        this.transform.position = cameraReletivePosition + chracterTrans.position;
        foreach (GameObject o in _objects2D)
        {
            o.transform.rotation = Quaternion.Euler(0, 90, 0) *o.transform.rotation;
        }
    }
    
}
