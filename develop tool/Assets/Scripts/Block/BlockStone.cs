using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStone : Block 
{
    void Start()
    {
        Hardness = setHardness(10);
    }
}
