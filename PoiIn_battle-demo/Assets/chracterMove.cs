using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chracterMove : MonoBehaviour
{
    private loadMap _loadMap;
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
    private void Start()
    {
        Debug.Log(thisPosition);
        _loadMap = FindObjectOfType<loadMap>();
         cam = GameObject.FindGameObjectWithTag("MainCamera");
         reletiveDistance = cam.GetComponent<cameraTrack>().cameraReletivePosition;
         _eventSinario = FindObjectOfType<eventSinario>();
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
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x, thisPosition.y, thisPosition.z + 1)))
                {
                    thisPosition = new Vector3Int(thisPosition.x, thisPosition.y,thisPosition.z + 1);
                    Debug.Log(thisPosition);
                    _eventSinario.checkSinario(thisPosition);
                    target2Dposition = transform.position + new Vector3(-0.3f, 0.15f, 0);
                    StartCoroutine(lerpMove());
                }

                break;
            case direction.left:
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x - 1,
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
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x,
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
                if (_loadMap._testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x + 1,
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
    public void chracterMoveTo(direction _direction)
    {
        Direction = _direction;
        confirm = true;
    }
    
}