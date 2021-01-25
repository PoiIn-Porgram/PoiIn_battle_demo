using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chracter2Dposition : MonoBehaviour
{
    public Vector3 get2Dposition(Vector3 _3Dposition)
    {
        return new Vector3((_3Dposition.x-_3Dposition.z)*0.3f,
            (_3Dposition.z+_3Dposition.x)*0.15f,
            _3Dposition.z+_3Dposition.x);
    }
}
