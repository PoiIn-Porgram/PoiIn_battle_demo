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
    private attackMK1 _attack1;
    private attackMK2 _attack2;
    private characterSpawner _characterSpawner = new characterSpawner();


    [SerializeField]
    private GameObject projectilePrefab;
    private void Awake()
    {
        //地图存档读取器，在awake周期提前触发
        _testMap = FindObjectOfType<testMap>();
        _testMap.LoadData();
    }

    private void Start()
    {
        GameObject mixieerBulang = GameObject.FindWithTag("mixieerBulang");
        //人物移动脚本
        //_chracterMove = FindObjectOfType<chracterMove>();
        _chracterMove = mixieerBulang.GetComponent<chracterMove>();
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
        //攻击脚本
        _attack1 = FindObjectOfType<attackMK1>();
        _attack2 = FindObjectOfType<attackMK2>();
        //角色生成脚本
        _characterSpawner = FindObjectOfType<characterSpawner>();
        //地图存档读取器，在awake周期提前触发
    }

    private int i = 1,j = 0;
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

       
        if(GUILayout.Button("motimono"))
        foreach (chrecterCard.motimono cardMotimono in _card.Motimonos)
        {
            Debug.Log(cardMotimono.name);
            Debug.Log(cardMotimono.description);
        }
        
        //K键自杀
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("dying");
            Debug.Log(_death.gameObject);
            _death.kill(GameObject.FindWithTag("Player"));
        }

        //P键换人
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("changecharacter!");
        }

        //左ALT攻击
        if (GUILayout.Button("attack1") || Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("attacking!");
            _attack1.attack(_chracterMove.gameObject, projectilePrefab, new Vector3(0, 0, 0), 0.1f);
        }
        if (GUILayout.Button("attack2") || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Debug.Log("attacking!");
            _attack2.dTime = 0;
            _attack2.attack(_chracterMove.gameObject, projectilePrefab, new Vector3(0, 0, _chracterMove.transform.position.z), 2f);
        }
        
        if (GUILayout.Button("damage"))
        {
            _card.changeStatus();
        }

        if (GUILayout.Button("spawn mixieerBulang"))
        {
            _characterSpawner.spawnerCharacter("mixieerBulang",new Vector3Int(j,0,i));
            i++;
            if (i>4)
            {
                j++;
                i = 0;
            }
        }

        if (GUILayout.Button("spawn Zago"))
        {
            _characterSpawner.spawnerCharacter("zako",new Vector3Int(j,0,i));
             i++;
            if (i>4)
            {
                j++;
                i = 0;
            }
        }

        if (GUILayout.Button("read scriptable object"))
        {
            var data = Resources.Load<Item>("Item");
            Debug.Log(data.AP);
        }
    }
    
}
