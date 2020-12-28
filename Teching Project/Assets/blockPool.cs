using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPool : MonoBehaviour
{
    public List<GameObject> blockTypeList;
    public Dictionary<Vector2Int, GameObject> Pool = new Dictionary<Vector2Int, GameObject>();
    [Serializable]
    public enum blockTpye
    {
        blue_block =0,
        green_block =1,
        pink_block = 2,
        red_block = 3,
        yellow_block = 4,
        blockGrey_yaojun = 5
    }//?

    private int initialNumberOfBlock;
    private GameObject _gameObject;
    private Vector2Int boardScale;
    private cluesCompiler _compiler;
    private blockTpye type;
    public void initializeBoard()
    {
        boardScale = GetComponent<chessBoardManager>().boradScale;
        initialNumberOfBlock = boardScale.x * boardScale.y;
        for (int i = 0; i < initialNumberOfBlock; i++)
        {
            distributeTheBlock(new Vector2Int(i / boardScale.x, i % boardScale.y),0);
        }                                            //占用太多的内存空间
    }
    public void distributeTheBlock(Vector2Int coordinate,int typeNum)
    {
        _gameObject = Instantiate(blockTypeList[typeNum], this.transform,false);
        _gameObject.transform.position =new Vector3(coordinate.x,0,coordinate.y)+transform.position;
        _gameObject.SetActive(true);
        if (Pool.ContainsKey(coordinate) == true)
        {
            Destroy(Pool[coordinate]);
            Pool[coordinate] = _gameObject;
        }
        else
        {
            Pool.Add(coordinate,_gameObject);
        }
       
    }

   /* public void recycleBoard(Vector2Int cellCoordinate)
    {
        if (Pool[cellCoordinate].activeSelf == true)
        {
            Pool[cellCoordinate].SetActive(false);
        }
    }//封印
    public void spawnBoard(Vector2Int cellCoordinate)
    {
        if (Pool[cellCoordinate].activeSelf == false)
        {
            Pool[cellCoordinate].SetActive(true);
        }//封印
      
    }*/
}
