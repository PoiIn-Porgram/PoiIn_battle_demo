using System.Diagnostics.Tracing;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LongRangeAttack1 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject groundtile = GameObject.Find("GroundTile");

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawLine(new Vector3(player.transform.position.x, player.transform.position.y-0.5f, 0), new Vector3(player.transform.position.x+20, player.transform.position.y+10-0.5f, 0), Color.red);
    }

    public void OnMouseClick(){

        GameObject player = GameObject.Find("player");
        GameObject chessboard = GameObject.Find("Chessboard");
        GameObject enemy = GameObject.Find("Enemy1");

        int xpos = player.GetComponent<CharacterController>().xpos;
        int ypos = player.GetComponent<CharacterController>().ypos;
        int inDex = player.GetComponent<CharacterController>().inDex;

        int xposE = enemy.GetComponent<EnemyController>().xpos;
        int yposE = enemy.GetComponent<EnemyController>().ypos;
        int inDexE = enemy.GetComponent<EnemyController>().inDex;

        // int roomNumberX = chessboard.GetComponent<chessBoardManager>().roomNumberX;
        // int roomNumberY = chessboard.GetComponent<chessBoardManager>().roomNumberY;


        RaycastHit2D[] hitsRU = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y-0.5f), new Vector2(2 , 1), 5f);
        RaycastHit2D[] hitsRD = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y-0.5f), new Vector2(2 , -1), 5f);
        RaycastHit2D[] hitsLU = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y-0.5f), new Vector2(-2 , 1), 5f);
        RaycastHit2D[] hitsLD = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y-0.5f), new Vector2(-2 , -1), 5f);


        foreach(var i in chessBoardManager.rooms){
            i.room.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
            i.room.GetComponent<EventTrigger>().enabled = false;

        }

            enemy.GetComponent<Renderer>().material.color = new Color32(200, 200, 200, 255);
            enemy.GetComponent<EventTrigger>().enabled = false;
        


        foreach(var i in hitsRU)
        {
            GameObject Hit = i.collider.gameObject;
            Hit.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            Hit.GetComponent<EventTrigger>().enabled = true;

        }
        foreach(var i in hitsRD)
        {
            GameObject Hit = i.collider.gameObject;
            Hit.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            Hit.GetComponent<EventTrigger>().enabled = true;

        }
        foreach(var i in hitsLU)
        {
            GameObject Hit = i.collider.gameObject;
            Hit.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            Hit.GetComponent<EventTrigger>().enabled = true;

        }
        foreach(var i in hitsLD)
        {
            GameObject Hit = i.collider.gameObject;
            Hit.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            Hit.GetComponent<EventTrigger>().enabled = true;

        }

        bool inRange = 
            yposE == ypos || xposE == xpos ;
        // Debug.Log(inRange);
        // Debug.Log(xposE);
        // Debug.Log(xpos);
        // Debug.Log(yposE);
        // Debug.Log(ypos);

        if(inRange){
            enemy.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            enemy.GetComponent<EventTrigger>().enabled = true;
        }

    }

}
