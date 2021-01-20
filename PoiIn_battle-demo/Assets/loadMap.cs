using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMap : MonoBehaviour
{
    public testMap _testMap;

    private void Awake()
    {

        _testMap = GetComponent<testMap>();
        //_testMap.initializeTestMap();
        _testMap.LoadData();
    }
}
