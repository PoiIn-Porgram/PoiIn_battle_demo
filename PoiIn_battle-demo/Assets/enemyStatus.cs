using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStatus : MonoBehaviour
{
    [Serializable]
    public struct _enemyStatus
    {
        public enum enemyType
        {
            Type_A,
            Type_B,
            Type_C
        }
        public Vector3Int statrPosition;
        public enemyType Type;
    }
    public _enemyStatus[] enemies;
    public List<GameObject> enemyList;
}
