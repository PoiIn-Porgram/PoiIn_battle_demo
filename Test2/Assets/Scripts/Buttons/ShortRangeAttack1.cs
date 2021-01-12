using System;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShortRangeAttack1 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject groundtile = GameObject.Find("GroundTile");

    }

    // Update is called once per frame
    void Update()
    {
        // BaseEventData.SelectObject;

    }

    public void OnMouseClick(){

        GameObject player = GameObject.Find("player");
        // player.GetComponent<CharacterController>();
        GameObject chessboard = GameObject.Find("Chessboard");

        GameObject enemy = GameObject.Find("Enemy1");



        foreach(var j in chessBoardManager.rooms){
            j.room.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
            j.room.GetComponent<EventTrigger>().enabled = false;
        }

        // foreach(var k in chessBoardManager.roles){
            enemy.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
            enemy.GetComponent<EventTrigger>().enabled = false;
        // }


        int xpos = player.GetComponent<CharacterController>().xpos;
        int ypos = player.GetComponent<CharacterController>().ypos;
        // print(ypos);
        // debug
        int inDex = player.GetComponent<CharacterController>().inDex;


        int xposE = enemy.GetComponent<EnemyController>().xpos;
        int yposE = enemy.GetComponent<EnemyController>().ypos;
        // print(ypos);
        // debug
        int inDexE = enemy.GetComponent<EnemyController>().inDex;


        int i;
        int roomNumberY = chessboard.GetComponent<chessBoardManager>().roomNumberY;
        // print(roomNumberY);
        // debug


        int Max = xpos+ypos+inDex
                + roomNumberY;
        int Min = xpos+ypos+inDex
                - roomNumberY;

        for(i = Min; i<= Max; i++){
            if(i>=0 && i< chessboard.GetComponent<chessBoardManager>().roomNumberX
            *chessboard.GetComponent<chessBoardManager>().roomNumberY){
                if( i == Min || i == Max){
                    // chessBoardManager.rooms[i].room.GetComponent<Ground>().Omenter();
                    chessBoardManager.rooms[i].room.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                    chessBoardManager.rooms[i].room.GetComponent<EventTrigger>().enabled = true;
                }
                // if(  i >= xpos+ypos+inDex -1
                //      &&
                //      i <= xpos+ypos+inDex +1
                // ){if(ypos == roomNumberY){

                //     }if(ypos == 0 ){

                //     }else{
                //         // chessBoardManager.rooms[i].room.GetComponent<Ground>().Omenter();
                //         // chessBoardManager.rooms[i].room.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //         // chessBoardManager.rooms[i].room.GetComponent<EventTrigger>().enabled = true;
                //     }
                // }

                if(ypos == (roomNumberY -1)){
                    // if(i >= xpos+ypos+inDex -1
                    //     &&
                    //     i <= xpos+ypos+inDex){
                        
                    //     Bright(i);
                    // }
                    Bright(xpos+ypos+inDex-1);
                    Bright(xpos+ypos+inDex);

                }else if(ypos == 0){
                    // if(i >= xpos+ypos+inDex
                    //     &&
                    //     i <= xpos+ypos+inDex +1){
                        
                    //     Bright(i);
                    // }
                    Bright(xpos+ypos+inDex);
                    Bright(xpos+ypos+inDex+1);
                }else{
                    // if(i >= xpos+ypos+inDex -1
                    //     &&
                    //     i <= xpos+ypos+inDex +1){
                        
                    //     Bright(i);
                    // }
                    Bright(xpos+ypos+inDex-1);
                    Bright(xpos+ypos+inDex);
                    Bright(xpos+ypos+inDex+1);
                }


                // if( ypos == roomNumberY){
                //     chessBoardManager.rooms[i-1].room.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //     chessBoardManager.rooms[i-1].room.GetComponent<EventTrigger>().enabled = true;
                // }
                // if( ypos == 0){
                //     chessBoardManager.rooms[i+1].room.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //     chessBoardManager.rooms[i+1].room.GetComponent<EventTrigger>().enabled = true;
                // }
            }
        }

        bool inRange = 
            xposE >= xpos-1 && xposE <= xpos+1 && yposE == ypos ||
            yposE >= ypos-1 && yposE <= ypos+1 && xposE == xpos ;
        // Debug.Log(inRange);
        if(inRange){
            enemy.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            enemy.GetComponent<EventTrigger>().enabled = true;
        }
    }
    public void Bright(int i){
        chessBoardManager.rooms[i].room.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        chessBoardManager.rooms[i].room.GetComponent<EventTrigger>().enabled = true;
    }
}