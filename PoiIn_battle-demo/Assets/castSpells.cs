using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castSpells : MonoBehaviour
{
    private chrecterCard _card;
    /// <summary>
    /// 本质上应该是一个比较数值的函数
    /// 目前还没做，只是引用了人物卡，留待规则层的同志们完善@aha@暴走の扎克
    /// </summary>
    private void Start()
    {
        _card = GetComponent<chrecterCard>();
    }


}
