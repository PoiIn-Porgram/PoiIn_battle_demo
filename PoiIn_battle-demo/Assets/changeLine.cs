using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeLine : MonoBehaviour
{

    
    public Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }
    
    /// <summary>
    /// 改变文本的接口函数
    /// </summary>
    public void changeLineTo(string line)
    {
        _text.text = line;
    }
}
