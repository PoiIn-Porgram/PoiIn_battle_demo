using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testController : MonoBehaviour
{
    
    /// <summary>
    /// 用于展示所有的函数接口，脚手架，可以参考引用方式
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
           _chracterMove.chracterMoveTo(chracterMove.direction.front);
        }
        if (GUILayout.Button("A"))
        {
            _chracterMove.chracterMoveTo(chracterMove.direction.left);
        }
        if (GUILayout.Button("S"))
        {
            _chracterMove.chracterMoveTo(chracterMove.direction.back);
        }
        if (GUILayout.Button("D"))
        {
            _chracterMove.chracterMoveTo(chracterMove.direction.right);
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
