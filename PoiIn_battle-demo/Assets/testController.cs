using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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
    private testMap _testMap;
    private characterDeath _death;
    private void Awake()
    {
        _testMap = FindObjectOfType<testMap>();
        _testMap.LoadData();
    }

    private void Start()
    {
        //人物移动脚本
        _chracterMove = FindObjectOfType<chracterMove>();
        //骰子
        _dice = FindObjectOfType<dice>();
        //Gal风格脚本控制器
        _sinarioController = FindObjectOfType<sinarioController>();
        //相机追踪脚本
        _cameraTrack = FindObjectOfType<cameraTrack>();
        //人物卡
        _card = FindObjectOfType<chrecterCard>();
        //死亡脚本
        _death = FindObjectOfType<characterDeath>();
        //地图存档读取器，在awake周期提前触发
        
    }

    private void OnGUI()
    {
        if (GUILayout.Button("W")||Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(_chracterMove.thisPosition);
           _chracterMove.chracterMoveTo(chracterMove.direction.front);
        }
        if (GUILayout.Button("A")||Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(_chracterMove.thisPosition);
            _chracterMove.chracterMoveTo(chracterMove.direction.left);
        }
        if (GUILayout.Button("S")||Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(_chracterMove.thisPosition);
            _chracterMove.chracterMoveTo(chracterMove.direction.back);
        }
        if (GUILayout.Button("D")||Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(_chracterMove.thisPosition);
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
        if(GUILayout.Button("motimono"))
        foreach (chrecterCard.motimono cardMotimono in _card.Motimonos)
        {
            Debug.Log(cardMotimono.name);
            Debug.Log(cardMotimono.description);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("dying");
            // _death.toKill = _chracterMove.gameObject;
            Debug.Log(_death.gameObject);
            _death.kill();
        }

        
    }
}
