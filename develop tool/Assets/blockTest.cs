using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockTest : Block
{
    void Start()
    {
        this.Hardness = this.setHardness(1);
        this.blockId = this.setBlockId(100);
        this.blockstyle = BlockStyle.blockStyle.TEST;
    }
}
