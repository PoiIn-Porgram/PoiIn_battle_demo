using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chracterMove : MonoBehaviour
{
    /// <summary>
    /// 提供移动函数的接口
    /// </summary>
    private loadMap _loadMap;
    private int chracterHeight = 1;
    private Vector3Int targetPosition,thisPosition = new Vector3Int(0,0,0);
    public float lerpFactor = 0.1f;
    /// <summary>
    /// 考虑减少硬编码
    /// </summary>
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
    private void Start()
    {
        _loadMap = FindObjectOfType<loadMap>();
         cam = GameObject.FindGameObjectWithTag("MainCamera");
         reletiveDistance = cam.GetComponent<cameraTrack>().cameraReletivePosition;
         _eventSinario = FindObjectOfType<eventSinario>();
    }

    IEnumerator lerpMove()
    {
        //协程中，使用插值制造相机的移动动画
        this.transform.position = Vector3.Lerp(transform.position, targetPosition, lerpFactor);
        cam.transform.position = Vector3.Lerp(cam.transform.position, this.transform.position + reletiveDistance, lerpFactor);
        if (Vector3.SqrMagnitude(targetPosition -  this.transform.position)<0.005f&&
            Vector3.SqrMagnitude(cam.transform.position - reletiveDistance - targetPosition)<0.00005f)
        {
            this.transform.position = targetPosition;
            cam.transform.position = reletiveDistance + targetPosition;
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
        //判断移动方向和移动的合法性。可以进一步封装
        switch (_direction)
        {
            case direction.front:
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x,
                                                                                thisPosition.y,
                                                                                thisPosition.z + 1)))
                    thisPosition.z += 1;
                
                targetPosition = thisPosition + new Vector3Int(0,chracterHeight,0);
                _eventSinario.checkSinario(thisPosition);
                StartCoroutine(lerpMove());
                break;
            case direction.left:
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x - 1,
                    thisPosition.y,
                    thisPosition.z )))
                    thisPosition.x -= 1;
                targetPosition = thisPosition + new Vector3Int(0,chracterHeight,0);
                _eventSinario.checkSinario(thisPosition);
                StartCoroutine(lerpMove());
                break;
            case direction.back:
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x,
                    thisPosition.y,
                    thisPosition.z - 1)))
                    thisPosition.z -= 1;
                targetPosition = thisPosition + new Vector3Int(0,chracterHeight,0);
                _eventSinario.checkSinario(thisPosition);
                StartCoroutine(lerpMove());
                break;
            case direction.right:
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x + 1,
                    thisPosition.y,
                    thisPosition.z )))
                    thisPosition.x += 1;
                targetPosition = thisPosition + new Vector3Int(0,chracterHeight,0);
                print(targetPosition);
                _eventSinario.checkSinario(thisPosition);
                StartCoroutine(lerpMove());
                break;
        }
    }

    public void chracterMoveTo(direction _direction)
    {
        Direction = _direction;
        confirm = true;
    }
}