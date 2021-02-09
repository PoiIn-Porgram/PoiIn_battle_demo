using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeBoard : MonoBehaviour
{
    /// <summary>
    /// 地图生成器
    /// 从testMap脚本中获得存档中的地图数据，即地图的位置和该位置的砖块种类，然后自动生成
    /// </summary>
    private cubeManager _cubeManager;
    private testMap _testMap;
    public struct blockCursor
    {
        public Vector3Int position;
        public int blockStyle;
    }
    private List<blockCursor> BlockCursorsOnLoad;
    
    private void Start()
    {
        BlockCursorsOnLoad = new List<blockCursor>();
        _cubeManager = GetComponent<cubeManager>();
        _testMap = FindObjectOfType<testMap>();
        blockCursor _blockCursor;
        foreach (KeyValuePair<Vector3Int,int> loadedBlock in _testMap.savedBlocks)
        {
            _blockCursor = new blockCursor();
            _blockCursor.position = loadedBlock.Key;
            _blockCursor.blockStyle = loadedBlock.Value;
            BlockCursorsOnLoad.Add(_blockCursor);
        }
       
        initializeChessBoard();
    }

    private void initializeChessBoard()
    {
        int sum = BlockCursorsOnLoad.Count;
        for (int i = 0; i < sum; i++)
        {
            spawnBlock(BlockCursorsOnLoad[0]);
            BlockCursorsOnLoad.Remove(BlockCursorsOnLoad[0]);
        }
    }
    // public GameObject thisBlock;
    void spawnBlock(blockCursor thisBlockCursor)
    {
        GameObject thisBlock;
        thisBlock = Instantiate(_cubeManager.cubeList[thisBlockCursor.blockStyle]);
        _cubeManager.allCubes.Add(thisBlock);
        // Debug.Log(_cubeManager.allCubes);
        thisBlock.SetActive(true);
        thisBlock.transform.SetParent(this.transform);
        //thisBlock.transform.position = thisBlockCursor.position;
        thisBlock.transform.position = get2Dposition(thisBlockCursor.position);
        thisBlock.GetComponent<cubeController>().abstractPosition = thisBlockCursor.position;
    }
    public Vector3 get2Dposition(Vector3 _3Dposition)
    {
        return new Vector3((_3Dposition.x-_3Dposition.z)*0.3f,
            (_3Dposition.z+_3Dposition.x)*0.15f,
            _3Dposition.z+_3Dposition.x);
    }

    

}
