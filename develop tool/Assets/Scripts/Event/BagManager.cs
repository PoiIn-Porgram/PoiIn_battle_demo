using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包管理器
/// </summary>
public class BagManager : MonoBehaviour {

    public GameObject[] cells;//背包的方格
    public GameObject hightlight;//高亮框
    public MapManager mapmanager;
    [HideInInspector]
    public Block nextblock;//下一个将要被玩家创建出来的方块
    int index = 0;
    public Sprite[] itemOnguiSprites;

    void Start()
    {
        //如果GUI上面的物品类型为空
        if(cells[0].GetComponentInChildren<ItemOnGUI>().itemonguiStyle==ItemOnGUIStyle.itemonGUIstyle.gui_EMPTY)
        {
            //则下一个将要被玩家创建的方块为空
            nextblock = null;
        }
        else
        {
            //否则下一个将要被玩家创建的方块为hightlight选择的类型
            CheckItemStyle(hightlight);
        }
    }
    void Update()
    {
        CheckItemStyle(hightlight);
        float h = Input.GetAxisRaw("Mouse ScrollWheel");
        if (h>0)
        {
            index--;
            if( index < 0)
            {
                index = 8;
            }
            hightlight.transform.parent = cells[index].transform;
            hightlight.transform.localPosition = Vector3.zero;
            CheckItemStyle(hightlight);
        }
        else if(h<0)
        {
            index++;
            if (index >=9)
            {
                index = 0;
            }
            hightlight.transform.parent = cells[index].transform;
            hightlight.transform.localPosition = Vector3.zero;
            CheckItemStyle(hightlight);
        }
    }

    /// <summary>
    /// 根据当前选择的UI物品的类型确定将要创建的方块的类型
    /// </summary>
    /// <param name="基准体"></param>
    public void CheckItemStyle(GameObject benchmark)
    {
        if (benchmark.transform.parent.GetComponentInChildren<ItemOnGUI>().itemonguiStyle == ItemOnGUIStyle.itemonGUIstyle.gui_DIRT)
        {
            nextblock = mapmanager.blocks[(int)BlockStyle.blockStyle.DIRT - 1];
        }
        if (benchmark.transform.parent.GetComponentInChildren<ItemOnGUI>().itemonguiStyle == ItemOnGUIStyle.itemonGUIstyle.gui_GRASS)
        {
            nextblock = mapmanager.blocks[(int)BlockStyle.blockStyle.GRASS - 1];
        }
        if (benchmark.transform.parent.GetComponentInChildren<ItemOnGUI>().itemonguiStyle == ItemOnGUIStyle.itemonGUIstyle.gui_STONE)
        {
            nextblock = mapmanager.blocks[(int)BlockStyle.blockStyle.STONE - 1];
        }
        if (benchmark.transform.parent.GetComponentInChildren<ItemOnGUI>().itemonguiStyle == ItemOnGUIStyle.itemonGUIstyle.gui_EMPTY)
        {
            nextblock = null;
        }
    }
}
