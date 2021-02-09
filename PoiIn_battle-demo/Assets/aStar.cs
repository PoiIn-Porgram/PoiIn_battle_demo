using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class aStar : MonoBehaviour
{
    public Vector3Int abstractPosition;
    private GameObject toAstar;
    public Vector3Int targetPosition;
    private float f;
    private float F;//用来比较
    private float g;
    private float h;
    public chracterMove.direction Direction;
    private int xMax;//地图json中似乎并没有地图大小的值，建议补上
    private int zMax;//地图中也没有一个所有地砖的列表，initial之后地砖就不可寻址了，有点麻烦
    public Vector3Int go2AbPosition;
    // private Vector3 go2RePosition;
    private cubeManager _cubeManager;
    private cubeController _cubeController;
    // private initializeBoard _initializeBoard;
    private chracterMove _chracterMove;

    void Start()
    {
        _cubeManager = FindObjectOfType<cubeManager>();
        _cubeController = FindObjectOfType<cubeController>();
        // _initializeBoard = FindObjectOfType<initializeBoard>();
        _chracterMove = FindObjectOfType<chracterMove>();

        toAstar = GameObject.Find("LekkerVerberens");
        abstractPosition = toAstar.GetComponent<chracterMove>().thisPosition;
        // targetPosition = new Vector3Int(4,0,2);
        // 改为public以便编辑器快速修改
        xMax = zMax = 5;//实际值
    }


    void Update()
    {
        
    }

    public void aiRoadFinder(){
        
        abstractPosition = toAstar.GetComponent<chracterMove>().thisPosition;
        go2AbPosition = abstractPosition;
        Direction = chracterMove.direction.hold;
        F=f=g=h=100;
        // g = 1;
        // h = (float)Math.Sqrt(
        //     Math.Pow(
        //         Math.Abs((targetPosition.x+1 - (abstractPosition.x+1))),
        //         2.0)+
        //     Math.Pow(
        //         Math.Abs((targetPosition.y+1 - (abstractPosition.y+1))),
        //         2.0)+
        //     Math.Pow(
        //         Math.Abs((targetPosition.z+1 - (abstractPosition.z+1))),
        //         2.0)
        // );
        // f = g + h;
        // Debug.Log(f);
        

        //z为纵\，x为横/
        Vector3Int abUp = abstractPosition + new Vector3Int(0,0,1);
        Vector3Int abDown = abstractPosition + new Vector3Int(0,0,-1);
        Vector3Int abLeft = abstractPosition + new Vector3Int(-1,0,0);
        Vector3Int abRight = abstractPosition + new Vector3Int(1,0,0);

        foreach(var i in _cubeManager.allCubes){
            // 这里可优化，遍历一遍费资源
            // if(abUp.x < xMax && abUp.z < zMax){
            // 好像不需要
            // if (_testMap.savedBlocks.ContainsKey(new Vector3Int(thisPosition.x, thisPosition.y, thisPosition.z + 1))){
            // 这个不错哈，可我不会使
            Vector3Int iAbPos = i.GetComponent<cubeController>().abstractPosition;//将地砖的AbPos记录，简化代码
            // Debug.Log(i.GetComponent<cubeController>().abstractPosition);
            if(iAbPos == abUp){
                g = 1;
                h = (float)Math.Sqrt(
                    Math.Pow(
                        Math.Abs((targetPosition.x+1 - (abUp.x+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.y+1 - (abUp.y+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.z+1 - (abUp.z+1))),
                        2.0)
                );
                f = g + h;
                Debug.Log(f);
                if(f<F){
                    F=f;
                    Direction = chracterMove.direction.front;
                    go2AbPosition = iAbPos;
                    // go2RePosition = chracter2Dposition.get2Dposition(iAbPos);
                }
            }
            if(iAbPos == abDown){
                g = 1;
                h = (float)Math.Sqrt(
                    Math.Pow(
                        Math.Abs((targetPosition.x+1 - (abDown.x+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.y+1 - (abDown.y+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.z+1 - (abDown.z+1))),
                        2.0)
                );
                // Debug.Log(abDown);
                f = g + h;
                Debug.Log(f);
                if(f<F){
                    F=f;
                    Direction = chracterMove.direction.back;
                    go2AbPosition = iAbPos;
                    // go2RePosition = chracter2Dposition.get2Dposition(iAbPos);
                }
            }
            if(iAbPos == abLeft){
                // Debug.Log(i.GetComponent<cubeController>().abstractPosition);
                g = 1;
                h = (float)Math.Sqrt(
                    Math.Pow(
                        Math.Abs((targetPosition.x+1 - (abLeft.x+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.y+1 - (abLeft.y+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.z+1 - (abLeft.z+1))),
                        2.0)
                );
                f = g + h;
                Debug.Log(f);
                if(f<F){
                    F=f;
                    Direction = chracterMove.direction.left;
                    go2AbPosition = iAbPos;
                    // go2RePosition = chracter2Dposition.get2Dposition(iAbPos);
                }
            }
            if(iAbPos == abRight){
                // Debug.Log(i.GetComponent<cubeController>().abstractPosition);
                g = 1;
                h = (float)Math.Sqrt(
                    Math.Pow(
                        Math.Abs((targetPosition.x+1 - (abRight.x+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.y+1 - (abRight.y+1))),
                        2.0)+
                    Math.Pow(
                        Math.Abs((targetPosition.z+1 - (abRight.z+1))),
                        2.0)
                );
                // Debug.Log(abRight);
                f = g + h;
                Debug.Log(f);
                if(f<F){
                    F=f;
                    Direction = chracterMove.direction.right;
                    go2AbPosition = iAbPos;
                    // go2RePosition = chracter2Dposition.get2Dposition(iAbPos);
                }
            }
        }
        // if(Direction == chracterMove.direction.hold){
            
        // }
        abstractPosition = go2AbPosition;
        // toAstar.GetComponent<chracterMove>().thisPosition = abstractPosition;
        toAstar.GetComponent<chracterMove>().chracterMoveTo(Direction);
        Debug.Log(toAstar.GetComponent<chracterMove>().thisPosition);

        //以下为启用循环
        // aiRoadFinder();
    }
}
