using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class dice : MonoBehaviour
{
    public int RollTheDice(int dimentions)
    {
        return Random.Range(1, dimentions+1) / 1;
    }
}
