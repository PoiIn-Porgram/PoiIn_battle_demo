using UnityEngine;
using System.Collections;

/// <summary>
/// 点击创造或破坏
/// </summary>
public class Point_Ray : MonoBehaviour {

    private MapManager instanse;

    private RaycastHit _hit;//射线采集器
    public BagManager bagManager;//背包管理器
    private Vector3 origin; //原点
    private Vector3 dir = Vector3.forward;//方向
    private float dis = 100;//距离
    private int layerMask = 1;//层;

    public Item[] items;//物品集合


    void Start()
    {
        instanse = GetComponent<MapManager>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))//右键
        {
            OnCreatBlock();
        }
        else if(Input.GetMouseButtonDown(0))//左键
        {
            OnDestroyBlock();
        }
    }
    /// <summary>
    /// 创建方块
    /// </summary>
    private void OnCreatBlock()
    {
        //1.使用鼠标点击发射射线(物品拾取)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条从主相机到鼠标点的射线
        bool resout = Physics.Raycast(ray, out _hit, 100);//发射一条射线，最大距离为100，返回过程中是否碰撞到对象，返回采集器信息
        if (resout)//如果中途有碰撞到对象
        {
            if(bagManager.nextblock==null)
            {
                return;
            }
            if(_hit.transform.tag=="Front"&&_hit.transform.parent.tag=="cube")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
            {
                instanse.CreatCubeByPlayer(bagManager.nextblock,_hit.transform.parent.GetComponent<Block>().blockFront());
            }
            else if (_hit.transform.tag == "Back" && _hit.transform.parent.tag == "cube")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
            {
                instanse.CreatCubeByPlayer(bagManager.nextblock, _hit.transform.parent.GetComponent<Block>().blockBack());
            }
            else if (_hit.transform.tag == "Left" && _hit.transform.parent.tag == "cube")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
            {
                instanse.CreatCubeByPlayer(bagManager.nextblock, _hit.transform.parent.GetComponent<Block>().blockLeft());
            }
            else if (_hit.transform.tag == "Right" && _hit.transform.parent.tag == "cube")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
            {
                instanse.CreatCubeByPlayer(bagManager.nextblock, _hit.transform.parent.GetComponent<Block>().blockRight());
            }
            else if (_hit.transform.tag == "Up" && _hit.transform.parent.tag == "cube")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
            {
                instanse.CreatCubeByPlayer(bagManager.nextblock, _hit.transform.parent.GetComponent<Block>().blockUp());
            }
            else if (_hit.transform.tag == "Down" && _hit.transform.parent.tag == "cube")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
            {
                instanse.CreatCubeByPlayer(bagManager.nextblock, _hit.transform.parent.GetComponent<Block>().blockDown());
            }
        }

        ////2.在空间中任意点发射一条射线
        //bool resout_1 = Physics.Raycast(origin, dir, out _hit, dis, layerMask);
        //if (resout_1)
        //{
        //    if (_hit.transform.tag == "player")//碰撞到的对象是不是想要的这个对象(通过标签来判断)
        //    {
        //        print(_hit.point);//打印出射线射在对象上的点的位置
        //        Debug.DrawLine(origin, _hit.point, Color.blue);//画线
        //    }
        //}
    }

    /// <summary>
    /// 摧毁方块
    /// </summary>
    private void OnDestroyBlock()
    {
        //1.使用鼠标点击发射射线(物品拾取)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条从主相机到鼠标点的射线
        bool resout = Physics.Raycast(ray, out _hit, 100);//发射一条射线，最大距离为100，返回过程中是否碰撞到对象，返回采集器信息
        if (resout)//如果中途有碰撞到对象
        {
            if (_hit.transform.parent.tag == "cube")
            {
                if(_hit.transform.parent.GetComponent<Block>().getBlockstyle() == BlockStyle.blockStyle.DIRT)
                {
                    _hit.transform.parent.GetComponent<Block>().OnBlockClicked(items[(int)BlockStyle.blockStyle.DIRT - 1]);
                }
                if (_hit.transform.parent.GetComponent<Block>().getBlockstyle() == BlockStyle.blockStyle.GRASS)
                {
                    _hit.transform.parent.GetComponent<Block>().OnBlockClicked(items[(int)BlockStyle.blockStyle.GRASS - 1]);
                }
                if (_hit.transform.parent.GetComponent<Block>().getBlockstyle() == BlockStyle.blockStyle.STONE)
                {
                    _hit.transform.parent.GetComponent<Block>().OnBlockClicked(items[(int)BlockStyle.blockStyle.STONE-1]);
                }
                if (_hit.transform.parent.GetComponent<Block>().getBlockstyle() == BlockStyle.blockStyle.TEST)
                {
                    print("testCube");
                    _hit.transform.parent.GetComponent<Block>().OnBlockClicked(items[(int)BlockStyle.blockStyle.TEST-1]);
                }
            }
        }
    }
}
