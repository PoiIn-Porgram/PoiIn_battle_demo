using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class textFileLoader : MonoBehaviour
{
    /// <summary>
    /// 用于保存存档的一个容器类
    /// </summary>
    public string[] sinario; //Application.dataPath + "/testMap.json"
    private void Start()
    {
        sinario = File.ReadAllLines(Application.dataPath + "/testSinario.txt",System.Text.Encoding.UTF8);
    }

    
}
