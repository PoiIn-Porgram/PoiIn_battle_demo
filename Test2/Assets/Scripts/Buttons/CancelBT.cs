using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelBT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseClick(){
        
        GameObject enemy = GameObject.Find("Enemy1");
        Bright();
        // Ground.toStart = true;
        foreach(var i in chessBoardManager.rooms){
            i.room.GetComponent<EventTrigger>().enabled = true;
            i.room.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        }

            enemy.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
            enemy.GetComponent<EventTrigger>().enabled = true;
    }



    public void Bright(){
        foreach(var i in chessBoardManager.rooms)
        {
            GameObject GB = i.room.gameObject;
            GB.GetComponent<Animator>().SetBool("isOver", false);
            // isOver = true;
        }
    }
}
