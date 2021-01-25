using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castSpells : MonoBehaviour
{
    private chrecterCard _card;
    /// <summary>
    /// 本质上应该是一个比较数值的函数
    /// </summary>
    private void Start()
    {
        _card = GetComponent<chrecterCard>();
    }


}
