using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chessBoardManager : MonoBehaviour
{

    public enum Direction{up, down, left, right};
    public Direction direction;
    
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Text txt;



    [Header("information")]
    public GameObject roomPerfab;
    public GameObject rolePerfab;
    public GameObject pointPerfab;
    public int roomNumberX;
    public int roomNumberY;
    public Color mainColor, SecondaryColor;

    [Header("toChange")]
    public GameObject roomPerfab2;
    public int toChangei;
    public int toChangej;

    
    [Header("positioning")]
    public Transform generatorPoint;
    public float xOffset, yOffset;


    [Header("timing")]
    public static int i=0;
    public static int j=0;
    public static int inDex=0;

    public double timeRest;
    public double lastTime;
    public double curTime;

    public float passedSeconds;

    [Header("speed")]

    public static float speed = 8;
    public float generatingSpeed;

    // [Header("pointing")]
    // public bool isOver = false;
    // public Animator FLoor;


    public struct Rooms{
        // public static List<GameObject> rooms = new List<GameObject>();
        // [SerializeField]
        public GameObject room;
        public int index;
        public int xpos;
        public int ypos;
        

    }

    public struct Roles{
        public GameObject role;
        public int index;
        public int xpos;
        public int ypos;
    }

    // public static List<GameObject> roles = new List<GameObject>();
    public static List<GameObject> points = new List<GameObject>();

    public static Rooms[] rooms;
    public static Roles[] roles;
    GameObject r;

    // public Rooms[] roomss; debug用


    // Start is called before the first frame update
    void Start()
    {
        rooms = new Rooms[roomNumberX*roomNumberY];
        roles = new Roles[10];
    }


    // Update is called once per frame
    void Update() { 

        timePass(100);
        curTime2 = Time.time;
        // roomss[inDex+i+j].room = Instantiate(roomPerfab2, generatorPoint.position, Quaternion.identity);

        curTime = Time.time;
        if(curTime - lastTime >= generatingSpeed)
        {
            //计时器部分
            timeRest = timeRest - generatingSpeed;
            lastTime = curTime;

            if(i < roomNumberX+1){
                if(j < roomNumberY){
                    if(i == toChangei -1 && j == toChangej -1){
                        // rooms.Add(Instantiate(roomPerfab2, generatorPoint.position, Quaternion.identity));
                        rooms[i+j+inDex].index = i+j+inDex;
                        rooms[i+j+inDex].xpos = i;
                        rooms[i+j+inDex].ypos = j;
                        rooms[i+j+inDex].room = Instantiate(roomPerfab2, generatorPoint.position, Quaternion.identity);
                        rooms[i+j+inDex].room.GetComponent<Ground>().xpos = i;
                        rooms[i+j+inDex].room.GetComponent<Ground>().ypos = j;
                        rooms[i+j+inDex].room.GetComponent<Ground>().inDex = inDex;

                    }else{
                        // points.Add(Instantiate(pointPerfab, generatorPoint.position, Quaternion.identity));
                        // rooms.Add(Instantiate(roomPerfab, generatorPoint.position, Quaternion.identity));
                        rooms[i+j+inDex].index = i+j+inDex;
                        rooms[i+j+inDex].xpos = i;
                        rooms[i+j+inDex].ypos = j;
                        // print(
                        //     rooms[i+j+inDex].ypos = j
                        // );
                        // debug
                        rooms[inDex+i+j].room = Instantiate(roomPerfab, generatorPoint.position, Quaternion.identity);
                        rooms[i+j+inDex].room.GetComponent<Ground>().xpos = i;
                        rooms[i+j+inDex].room.GetComponent<Ground>().ypos = j;
                        rooms[i+j+inDex].room.GetComponent<Ground>().inDex = inDex;
                        // print(rooms[i+j+inDex].room.GetComponent<Ground>().inDex);
                        // debug

                        // rooms[inDex+i+j].transform.parent = 
                    }
                    
                    rb = rooms[inDex+j+i].room.GetComponent<Rigidbody2D>();

                    // 显示层级
                    rooms[inDex+j+i].room.transform.position = new Vector3
                        (
                            rooms[inDex+j+i].room.transform.position.x,
                            rooms[inDex+j+i].room.transform.position.y,
                            (roomNumberX*roomNumberY - rooms[inDex+j+i].room.transform.position.z-inDex-j-i +1)
                        );

                    // 加速运动，减速由gameobject的rigidbody2d的linearlag完成
                    rb.velocity = new Vector2(0,speed);

                    //改变point位置
                    ChangePointPos(0);


                    // Debug.Log(rooms[inDex+j+i].transform.GetSiblingIndex());

                    // Debug.Log(inDex+i+j);
                    // Debug.Log(16-inDex-i-j);
                    
                    j++;
                }if(j == roomNumberY){

                    ChangePointPos(1);
                    i=i+1;
                    for(int k=0; k<roomNumberY;k++){
                        ChangePointPos(2);
                    }
                    j=0;
                    inDex = inDex + roomNumberY-1;
                    // Debug.Log(inDex);
                }
                if(i == roomNumberX && j == 0){
                    GameObject rolepoint = Instantiate(pointPerfab, new Vector3(0,0,0), Quaternion.identity);
                    roles[0].role = Instantiate(rolePerfab, rolepoint.transform.position, Quaternion.identity);

                    roles[0].role.name = "player";

                    roles[0].role.transform.position = new Vector3
                        (
                            roles[0].role.transform.position.x,
                            (roles[0].role.transform.position.y+2),
                            0
                        );
                    // roles[0].transform.SetSiblingIndex(0);
                    Destroy(rolepoint);

                    rb = roles[0].role.GetComponent<Rigidbody2D>();
                    rb.velocity = new Vector2(0,-speed);
                    i=i+1;
                    j=j+1;
                }
            }

        }

        txt.text = timeRest2.ToString();
    }


    public void ChangePointPos(int direct)
    {
        direction = (Direction) direct;
        // Random.range报错，需用UnityEngine。Random。Range（R大写）

        switch(direction)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(-xOffset, -yOffset, 0);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(xOffset, -yOffset, 0);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(xOffset, yOffset, 0);
                break;
        }
    }

    
    public float curTime2,lastTime2,timeRest2=100;
    // 原计时器
    void timePass(double passedSeconds)
    {   

        if (curTime2 - lastTime2 >= 1f)
        {   
            timeRest2 = timeRest2 - 1f;
            lastTime2 = curTime2;
            // Debug.Log(timeRest2);
        }


    }

    // public void OnMouseEnter() {
    //     anim.SetBool("isOver", true);
    // }

    // public void OnMouseExit() {
    //     anim.SetBool("isOver", false);
    // }

}
