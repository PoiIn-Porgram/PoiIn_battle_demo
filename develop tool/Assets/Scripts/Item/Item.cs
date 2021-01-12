using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum itemStyle
{
    /// <summary>
    /// 泥土物品
    /// </summary>
    item_Dirt,
    /// <summary>
    /// 石头物品
    /// </summary>
    item_Stone,
    /// <summary>
    /// 草地物品
    /// </summary>
    item_Grass
}
public class Item : MonoBehaviour {

    public float rotateSpeed { get; set; }
    Quaternion currentItemPos;
    public itemStyle itemstyle;//物品类型

    /// <summary>
    /// 设置item转动速度
    /// </summary>
    /// <param name="value"></param>
    public void setRotatespeed(int value)
    {
        rotateSpeed = value;
    }

    public float getRotateMovespeed()
    {
        return rotateSpeed;
    }


    void Start()
    {
        currentItemPos = gameObject.transform.localRotation;
    }
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        gameObject.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// 从方块上获得物品
    /// </summary>
    /// <param name="item"></param>
    public virtual void getItemFromBlock(Item item)
    {

    }

    /// <summary>
    /// 为生成的item添加弹起的效果
    /// </summary>
    /// <param name="赋予的力"></param>
    public void addForce(Rigidbody rig, float force)
    {
       rig.AddForce(Vector3.up * force);
    }
}
