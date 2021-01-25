using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMap : MonoBehaviour
{
    public testMap _testMap;
    /// <summary>
    /// 在awake周期，提前完成地图的提取
    /// </summary>
    private void Awake()
    {

        _testMap = GetComponent<testMap>();
        //_testMap.initializeTestMap();
        _testMap.LoadData();
    }
}
