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
    private void Start()
    {
        chracterTrans = FindObjectOfType<chracterMove>().GetComponentInParent<Transform>();
        _objects2D = GameObject.FindGameObjectsWithTag("2D_OBJ");

    }
    
    
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
