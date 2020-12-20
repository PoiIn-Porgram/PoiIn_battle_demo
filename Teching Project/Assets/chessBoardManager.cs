using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoardManager : MonoBehaviour
{
    public Vector2Int boradScale = new Vector2Int(9,9);
    public int blockSize = 1;

    private blockPool pool;

    private void Start()
    {
        pool = GetComponent<blockPool>();
    }

    private void Update()
    {
        for (int i = 0; i < boradScale.x; i++)
        {
            for (int j = 0; j < boradScale.y; j++)
            {
                Vector3 vec = Camera.main.WorldToViewportPoint(pool.Pool[new Vector2Int(i,j)].transform.position);
                if (vec.x > -0.2 && vec.x < 1.2 && vec.y > -0.2 && vec.y < 1.2)
                {
                   pool.spawnBoard(new Vector2Int(i,j));
                }
                else
                {
                    pool.recycleBoard(new Vector2Int(i,j));
                }
            }                                //性能开销太大了，后期优化，需要优化的部分有，减少克隆体的数量，优化映射地图的结构
                                             
        }
    }

    
}