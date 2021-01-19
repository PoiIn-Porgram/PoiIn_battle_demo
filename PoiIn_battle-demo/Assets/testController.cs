using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testController : MonoBehaviour
{
    private chracterMove _chracterMove;
    private dice _dice;
    private sinarioController _sinarioController;
    private void Start()
    {
        _chracterMove = FindObjectOfType<chracterMove>();
        _dice = FindObjectOfType<dice>();
        _sinarioController = FindObjectOfType<sinarioController>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("W"))
        {
            _chracterMove.Direction = chracterMove.direction.front;
            _chracterMove.confirm = true;
        }
        if (GUILayout.Button("A"))
        {
            _chracterMove.Direction = chracterMove.direction.left;
            _chracterMove.confirm = true;
        }
        if (GUILayout.Button("S"))
        {
            _chracterMove.moveTo(chracterMove.direction.back);
        }
        if (GUILayout.Button("D"))
        {
            _chracterMove.Direction = chracterMove.direction.right;
            _chracterMove.confirm = true;
        }

        if (GUILayout.Button("Dice"))
        {
            Debug.Log(_dice.RollTheDice(6));
        }

        if (GUILayout.Button("nextSinario"))
        {
            _sinarioController.nextSinario();
        }
    }
}
