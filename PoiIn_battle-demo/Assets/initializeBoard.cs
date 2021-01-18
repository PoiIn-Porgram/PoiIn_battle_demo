using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeBoard : MonoBehaviour
{
    private cubeManager _cubeManager;
    private loadMap _loadMap;
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
        _loadMap = FindObjectOfType<loadMap>();
        blockCursor _blockCursor;
        foreach (KeyValuePair<Vector3Int,int> loadedBlock in _loadMap._testMap.savedBlocks)
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
    void spawnBlock(blockCursor thisBlockCursor)
    {
        GameObject thisBlock;
        thisBlock = Instantiate(_cubeManager.cubeList[thisBlockCursor.blockStyle]);
        thisBlock.SetActive(true);
        thisBlock.transform.SetParent(this.transform);
        thisBlock.transform.position = thisBlockCursor.position;
        

    }
}
