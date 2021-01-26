using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chracterMove : MonoBehaviour
{
    /// <summary>
    /// 人物移动脚本
    /// 人物移动脚本的难点在于，为了便于AI的制作，格子的坐标应当为整数，但是基于2D的unity引擎格子的Transform.position一定不是整数
    /// 所以讲两套坐标相互隔离，实例层使用真实坐标，规则层使用抽象坐标
    /// thisPosition维护角色在抽象坐标中的位置，由target2Dposition维护真实坐标
    /// 但是这种方式会造成较大的偏移，详见下文代码的注释处
    /// 希望后续能修改那部分的代码
    /// </summary>
    
    private int chracterHeight = 1;
    public Vector3Int thisPosition = new Vector3Int(0,0,0);
    public Vector3 _2Dposition = new Vector3Int(0,0,0),target2Dposition;
    public float lerpFactor = 0.1f;
    public enum direction
    {
        front,
        left,
        back,
        right
    }

    private GameObject cam;
    private Vector3 reletiveDistance;
    private eventSinario _eventSinario;
    private testMap _testMap;
    private void Start()
    {
        Debug.Log(thisPosition);
        cam = GameObject.FindGameObjectWithTag("MainCamera");
         reletiveDistance = cam.GetComponent<cameraTrack>().cameraReletivePosition;
         _eventSinario = FindObjectOfType<eventSinario>();
         _testMap = FindObjectOfType<testMap>();
    }

    IEnumerator lerpMove()
    {
        this.transform.position = Vector3.Lerp(transform.position, target2Dposition, lerpFactor);
        cam.transform.position = Vector3.Lerp(cam.transform.position, this.transform.position + reletiveDistance, lerpFactor);
        if (Vector3.SqrMagnitude(target2Dposition -  this.transform.position)<0.005f&&
            Vector3.SqrMagnitude(cam.transform.position - reletiveDistance - target2Dposition)<0.00005f)
        {
            this.transform.position = target2Dposition;
            cam.transform.position = reletiveDistance + target2Dposition;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(lerpMove());
        }
        yield return 0;
    }
    
    public bool confirm = false;
    public direction Direction;
    private void Update()
    {
        if (confirm == true)
        {
            confirm = false;
            moveTo(Direction);
        }
        
    }

    public void  moveTo(direction _direction)
    {
        switch (_direction)
        {
            case direction.front:
                if (_testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x, thisPosition.y, thisPosition.z + 1)))
                {
                    //由美术组给的砖块的形状，根据计算得到如下的公式
                    //抽象坐标向前＋1，虚拟坐标需要左移0.3，上移0.15，其他方向以此类推
                    thisPosition = new Vector3Int(thisPosition.x, thisPosition.y,thisPosition.z + 1);
                    _eventSinario.checkSinario(thisPosition);
                    target2Dposition = transform.position + new Vector3(-0.3f, 0.15f, 0);
                    StartCoroutine(lerpMove());
                }
                break;
            case direction.left:
                if (_testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x - 1,
                    thisPosition.y,
                    thisPosition.z)))
                {

                    thisPosition = new Vector3Int(thisPosition.x - 1,
                        thisPosition.y,
                        thisPosition.z);
                    target2Dposition = transform.position + new Vector3(-0.3f, -0.15f, 0);
                    _eventSinario.checkSinario(thisPosition);
                    StartCoroutine(lerpMove());
                }

                break;
            case direction.back:
                if (_testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x,
                    thisPosition.y,
                    thisPosition.z - 1)))
                {

                    thisPosition = new Vector3Int(thisPosition.x,
                        thisPosition.y,
                        thisPosition.z - 1);
                    target2Dposition = transform.position + new Vector3(0.3f, -0.15f, 0);
                    _eventSinario.checkSinario(thisPosition);
                    StartCoroutine(lerpMove());
                }

                break;
            case direction.right:
                if (_testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x + 1,
                    thisPosition.y,
                    thisPosition.z)))
                {
                    thisPosition = new Vector3Int(thisPosition.x + 1,
                        thisPosition.y,
                        thisPosition.z);
                    target2Dposition = transform.position + new Vector3(0.3f, 0.15f, 0);
                    _eventSinario.checkSinario(thisPosition);
                    StartCoroutine(lerpMove());
                }
                break;
        }
    }
    /// <summary>
    /// 角色移动封装函数
    /// 感觉封装的不是特别好
    /// </summary>
    /// <param name="_direction"></param>
    public void chracterMoveTo(direction _direction)
    {
        Direction = _direction;
        confirm = true;
    }
    
}