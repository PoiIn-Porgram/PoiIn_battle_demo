using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class cluesCompiler : MonoBehaviour
{
    public enum blockTpye
    {
        blue_block_default = 0,
        green_block = 1,
        pink_block = 2,
        red_block = 3,
        yellow_block = 4,
        blockGrey_yaojun = 5
    }
    [Serializable]
    public struct clue 
    {
        public Vector2Int coordinate;
        public blockTpye type;
    }
    public List<clue> cluesList;
    
    private blockPool Pool;
    public void Awake()
    {
        Pool = GetComponent<blockPool>();
    }

    public void distributeClue()
    {
        if (cluesList.Count == 0)
        {
            return;
        }
        Debug.Log(cluesList[0].coordinate);
        Pool.distributeTheBlock(cluesList[0].coordinate,(int)cluesList[0].type);
        cluesList.Remove(cluesList[0]);
        distributeClue();
    }
}
