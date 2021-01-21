using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinarioController : MonoBehaviour
{
    /// <summary>
    /// 用于播放影响
    /// </summary>
    private textFileLoader _textFile;
    public int index = 0;
    private changeName _changeName;
    private changeLine _changeLine;
    private void Start()
    {
        _textFile = GetComponent<textFileLoader>();
        _changeName = FindObjectOfType<changeName>();
        _changeLine = FindObjectOfType<changeLine>();
    }
    
    public void nextSinario()
    {
        if (index == _textFile.sinario.Length)
        {
            return;
        }
        string[] thisSinario = _textFile.sinario[index].Split('|');
        switch (thisSinario[0])
        {
            case "*":
                Debug.Log(thisSinario[1]);
                index++;
                nextSinario();
                break;
            case "N":
                _changeLine.changeLineTo("");
                //_changeName._text.text = "";等效，但是减少全局变量
                _changeLine._text.text = thisSinario[1];
                index++;
                break;
            case "C":
                index++;
                nextSinario();
                Debug.Log(thisSinario[1]);
                break;
            case "D":
                _changeName._text.text=thisSinario[1];
                _changeLine._text.text = thisSinario[2];
                index++;
                break;
            default:
                index++;
                break;
        }
    }
    
}
