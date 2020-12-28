using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoardManager : MonoBehaviour
{
    public Vector2Int boradScale = new Vector2Int(9,9);
    public int blockSize = 1;
    public bool confirm = false;

    private blockPool pool;
    private cluesCompiler compiler;
    private void Start()
    {
        compiler = GetComponent<cluesCompiler>();
        pool = GetComponent<blockPool>();
        pool.initializeBoard();
        compiler.distributeClue();
    }
}