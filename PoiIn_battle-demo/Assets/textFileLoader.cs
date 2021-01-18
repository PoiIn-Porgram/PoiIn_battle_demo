using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class textFileLoader : MonoBehaviour
{
    public string[] sinario; //Application.dataPath + "/testMap.json"

    private void Start()
    {
        readFile();
    }

    private void readFile()
    {
        sinario = File.ReadAllLines(Application.dataPath + "/testSinario.txt",System.Text.Encoding.UTF8);
    }
    
}
