using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text showPlayerPosText;
    [HideInInspector]
    public bool ShowCurrentPos;
    /// <summary>
    /// 地图（世界）管理器
    /// </summary>
    public MapManager mapmanager;
    /// <summary>
    /// 背包管理器
    /// </summary>
    public BagManager bagmanager;

    public void Update()
    {
        WhetherToDisplayPlayerCurrentPos(true);
    }

    /// <summary>
    /// 是否允许显示玩家当前坐标
    /// </summary>
    public void WhetherToDisplayPlayerCurrentPos(bool isShow)
    {
        ShowCurrentPos = isShow;
        if (ShowCurrentPos)
        {
            showPlayerPosText.gameObject.SetActive(true);
            ShowPlayerCurrentPos();
        }
        else
        {
            showPlayerPosText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 在屏幕左上角实时显示玩家当前位置(在Update中调用)
    /// </summary>
    public void ShowPlayerCurrentPos()
    {
        Vector3 Pos = transform.position;
        float x = Pos.x;
        float y = Pos.y;
        float z = Pos.z;
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        z = Mathf.Round(z);
        showPlayerPosText.text = "位置：  " + x.ToString() + "  ," + y.ToString() + "  ," + z.ToString();
    }

    public void OnTriggerEnter(Collider col)
    {
        CollectItem(col);
    }

    /// <summary>
    /// 收集摧毁方块时掉落的物品
    /// </summary>
    /// <param name="碰撞到的实体"></param>
    public void CollectItem(Collider item)
    {
        List<GameObject> currentEmptycell = new List<GameObject>();
        if (item.transform.tag == "item")//如果玩家碰到的实体是item
        {
            Destroy(item.gameObject);//删除实体item
            for (int i = 0; i < bagmanager.cells.Length; i++)
            {
                //只有当背包的格子的类型为空的时候,才显示
                if (bagmanager.cells[i].GetComponentInChildren<ItemOnGUI>().itemonguiStyle == ItemOnGUIStyle.itemonGUIstyle.gui_EMPTY)
                {
                    currentEmptycell.Add(bagmanager.cells[i]);
                    if (item.transform.GetComponent<Item>().itemstyle == itemStyle.item_Dirt)//则判断item的类型是什么
                    {
                        //根据拾取的item类型在背包的格子中显示对应的itemOnui图标（必须为所有空格子的第一个空格）
                        currentEmptycell[0].GetComponentInChildren<ItemOnGUI>().image.enabled = true;
                        currentEmptycell[0].GetComponentInChildren<ItemOnGUI>().itemonguiStyle = ItemOnGUIStyle.itemonGUIstyle.gui_DIRT;
                        currentEmptycell[0].GetComponentInChildren<ItemOnGUI>().image.sprite = bagmanager.itemOnguiSprites[(int)itemStyle.item_Dirt];
                        //currentEmptycell[0].GetComponentInChildren<ItemOnGUI>().item_num.gameObject.SetActive(true);
                        //currentEmptycell[i].GetComponentInChildren<ItemOnGUI>().item_num.text = (int.Parse(currentEmptycell[0].GetComponentInChildren<ItemOnGUI>().item_num.text) + 1).ToString();
                    }
                }
                else//如果不为空
                {
                   //if( bagmanager.cells[i].GetComponentInChildren<ItemOnGUI>().itemonguiStyle == ItemOnGUIStyle.itemonGUIstyle.gui_DIRT)
                   // {
                        
                   // }
                }
            }
        }
    }
}
