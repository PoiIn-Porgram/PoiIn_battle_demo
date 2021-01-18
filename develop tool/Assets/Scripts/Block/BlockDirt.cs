using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDirt : Block {
    void Start()
    {
        this.Hardness = this.setHardness(1);
        this.blockId = this.setBlockId(0);
        
    }
    
}
