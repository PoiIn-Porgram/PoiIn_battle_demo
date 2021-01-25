using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    /// <summary>
    /// 方块能否被破坏的属性
    /// </summary>
    public bool BlockUnbreakable { get; set; }//无法被破坏属性
    /// <summary>
    /// 方块类型
    /// </summary>
    public BlockStyle.blockStyle blockstyle;
    /// <summary>
    /// 方块坚硬程度
    /// </summary>
    public int Hardness { get; set; }
    /// <summary>
    /// 方块ID
    /// </summary>
    public int blockId { get; set; }

    #region
    /// <summary>
    /// 获得此方块的上方位置
    /// </summary>
    /// <returns></returns>
    public Vector3 blockUp()
    {
        return new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
    }
    /// <summary>
    /// 获得此方块的下方位置
    /// </summary>
    /// <returns></returns>
    public Vector3 blockDown()
    {
        return new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1, this.gameObject.transform.position.z);
    }
    /// <summary>
    /// 获得此方块的左方位置
    /// </summary>
    /// <returns></returns>
    public Vector3 blockLeft()
    {
        return new Vector3(this.gameObject.transform.position.x - 1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
    /// <summary>
    /// 获得此方块的右方位置
    /// </summary>
    /// <returns></returns>
    public Vector3 blockRight()
    {
        return new Vector3(this.gameObject.transform.position.x + 1, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
    /// <summary>
    /// 获得此方块的前方位置
    /// </summary>
    /// <returns></returns>
    public Vector3 blockFront()
    {
        return new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z + 1);
    }
    /// <summary>
    /// 获得此方块的后方位置
    /// </summary>
    /// <returns></returns>
    public Vector3 blockBack()
    {
        return new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 1);
    }
    #endregion

    /// <summary>
    /// 设置方块坚硬程度
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int setHardness(int value)
    {
        return Hardness = value;
    }

    /// <summary>
    /// 获得方块坚硬程度
    /// </summary>
    /// <returns></returns>
    public int getHardness()
    {
        return Hardness;
    }

    /// <summary>
    /// 设置方块是否可以被破坏属性
    /// </summary>
    public bool setBlockUnbreakable(bool state)
    {
        return BlockUnbreakable = state;
    }
    /// <summary>
    /// 获得方块是否可以被破坏属性
    /// </summary>
    public bool getBlockUnbreakable()
    {
        return BlockUnbreakable;
    }

    /// <summary>
    /// 设置blockID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int setBlockId(int id)
    {
        return blockId = id;
    }

    /// <summary>
    /// 获得blockID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int getBlockId()
    {
        return blockId;
    }

    /// <summary>
    /// 获取当前方块的类型
    /// </summary>
    /// <returns></returns>
    public BlockStyle.blockStyle getBlockstyle()
    {
        return blockstyle;
    }

    /// <summary>
    /// 获取当前方块的位置
    /// </summary>
    /// <returns></returns>
    public Vector3 getCurrentBlockPos()
    {
        return this.gameObject.transform.position;
    }

    /// <summary>
    /// 摧毁一个方块
    /// </summary>
    public virtual void OnBlockDestroyed(Item item)
    {
        if(BlockUnbreakable==true)
        {
            Hardness--;
            if (Hardness <= 0)
            {
                Destroy(gameObject,0.3f);
                BlockDropOnDestroed(item);
            }
        }
    }

    /// <summary>
    /// 当玩家左击一个方块时
    /// </summary>
    public virtual void OnBlockClicked(Item item)
    {
        OnBlockDestroyed(item);
    }
    /// <summary>
    /// 当玩家右击一个方块时
    /// </summary>
    public virtual void OnBlockActivated()
    {

    }

    /// <summary>
    /// 设置自身的材质
    /// </summary>
    /// <param name="material"></param>
    public virtual void SetMaterial(Material m)
    {
        
    }

    /// <summary>
    /// 当方块被摧毁时掉落物品
    /// </summary>
    public void BlockDropOnDestroed(Item block)
    {
        Item newitem= Instantiate(block, getCurrentBlockPos(), block.transform.rotation);
        newitem.setRotatespeed(45);
        Rigidbody rig = newitem.GetComponent<Rigidbody>();
        newitem.addForce(rig, 800);
    }

    
}
