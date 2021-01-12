using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnemyController : MonoBehaviour
{
    public int xpos;
    public int ypos;
    public int inDex;
    public GameObject gobj;
    public int health;
    public int damage = 25;


    // Start is called before the first frame update
    void Start()
    {
        // health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //判断死亡
        if(health <= 0){
            Destroy(gobj);
        }
    }

}
