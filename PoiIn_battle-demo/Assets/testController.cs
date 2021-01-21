using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testController : MonoBehaviour
{
    
    /// <summary>
    /// 用于展示所有的函数接口
    /// </summary>
    
    private chracterMove _chracterMove;
    private dice _dice;
    private sinarioController _sinarioController;
    private cameraTrack _cameraTrack;
    private chrecterCard _card;
    private void Start()
    {
        _chracterMove = FindObjectOfType<chracterMove>();
        _dice = FindObjectOfType<dice>();
        _sinarioController = FindObjectOfType<sinarioController>();
        _cameraTrack = FindObjectOfType<cameraTrack>();
        _card = FindObjectOfType<chrecterCard>();
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
        
        if (GUILayout.Button("rotate"))
        {
           _cameraTrack.RotateTheCamera(1);
        }

        foreach (chrecterCard.spells spell in _card.spelleList)
        {
            if (GUILayout.Button(spell.spellName))
            {
                Debug.Log(spell.description);
            }
        }
    }
}
