using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=new Color(0,1,0,1);
        Gizmos.DrawCube(transform.position,Vector3.one);
    }
}
