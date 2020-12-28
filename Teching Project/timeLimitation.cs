using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLimitation : MonoBehaviour
{
    public int timerest = 60;
    private int onenum, tennum;
    [Serializable]
    public struct timeholder
    {
        public GameObject numplace;
        public GameObject[] picture; 
    }
    public timeholder[] placeholder;
   
}
