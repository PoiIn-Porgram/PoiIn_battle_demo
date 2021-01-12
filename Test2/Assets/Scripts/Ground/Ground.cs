using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour

{
    [SerializeField]
    
    public Animator ani;

    public Rigidbody2D rb;

    public GameObject rb2bLongATK;
    public GameObject player;
    // public Rigidbody2D rbPlayer;
    public GameObject enemy;


    public GameObject gobj;

    public CapsuleCollider2D cap;

    public bool isOver=false;
    public int isOverId = -1;

    public float Offset;

    public float posZ;

    public int xpos;
    public int ypos;
    public int inDex;


    // Start is called before the first frame update
    void Start()
    {
        StartListeners();

    }

    // Update is called once per frame
    void Update()
    {
        isOverId = Animator.StringToHash ("isOver");
        ani.SetBool (isOverId,isOver);
     
    }

    private void OMEnter(BaseEventData pointData) {

        // ani.SetBool("isOver", true);
        // isOver = true;
        // posZ = gobj.transform.position.z;
        // MoveUp();
        // gobj.transform.position = new Vector3(gobj.transform.position.x, gobj.transform.position.y, posZ);
        Omenter();
    }

    public void Omenter(){
        ani.SetBool("isOver", true);
        isOver = true;
        posZ = gobj.transform.position.z;
        MoveUp();
        gobj.transform.position = new Vector3(gobj.transform.position.x, gobj.transform.position.y, posZ);
    }
    

    private void OMExit(BaseEventData pointData) {
        
        ani.SetBool("isOver", false);
        isOver = false;
        MoveDown();
        gobj.transform.position = new Vector3(gobj.transform.position.x, gobj.transform.position.y, posZ);
        //不知道啥原理但必须两句都要,也必须是posZ而不能是gobj.transform.position.z

    }

    private void OMClick(BaseEventData pointData){
        if(gobj.CompareTag("Ground")){
            IfGround();
        }else if(gobj.CompareTag("Enemy")){
            IfEnemy();
        }
        
    }

    public void IfGround(){
        player = GameObject.Find("player");
        player.GetComponent<CharacterController>().xpos = xpos;
        player.GetComponent<CharacterController>().ypos = ypos;
        player.GetComponent<CharacterController>().inDex = inDex;
        // print(player.GetComponent<CharacterController>().ypos);


        // gobj = pointData.selectedObject.GetComponent<GameObject>();

        player.transform.position = rb.transform.position;
            player.transform.position = new Vector3
        (
            rb.transform.position.x,
            (rb.transform.position.y+1.8f),
            0
        );
        Rigidbody2D rbPlayer = player.GetComponent<Rigidbody2D>();
        rbPlayer.velocity = new Vector2(0,-chessBoardManager.speed);

        rb2bLongATK = rb.gameObject;//这句忘了是干嘛的了

        // GameObject gobj = rb.GetComponent<GameObject>();
    }

    public void IfEnemy(){
        player = GameObject.Find("player");
        enemy = GameObject.Find("Enemy1");
        // int health = enemy.GetComponent<EnemyController>().health;
        // int damage = player.GetComponent<CharacterController>().damage;
        //注意，enemy与player位置

        enemy.GetComponent<EnemyController>().health -= player.GetComponent<CharacterController>().damage;
    }

    public void MoveUp(){
        gobj.transform.position = new Vector2(gobj.transform.position.x,gobj.transform.position.y+Offset);
        
        cap.size = new Vector2(0.8f,1f);
        cap.offset = new Vector2(0f,-0.4f);
    }

    public void MoveDown(){
        gobj.transform.position = new Vector2(gobj.transform.position.x,gobj.transform.position.y-Offset);
        
        cap.size = new Vector2(0.8f,0.8f);
        cap.offset = new Vector2(0f,-0.1f);
    }


    public void StartListeners(){
        EventTrigger trigger = gobj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();

        // 鼠标点击事件
        entry1.eventID = EventTriggerType.PointerClick;
        // 鼠标进入事件 
        entry2.eventID = EventTriggerType.PointerEnter;
        // 鼠标滑出事件 
        entry3.eventID = EventTriggerType.PointerExit;

        entry1.callback = new EventTrigger.TriggerEvent();
        entry1.callback.AddListener(OMClick);
        trigger.triggers.Add(entry1);

        entry2.callback = new EventTrigger.TriggerEvent();
        entry2.callback.AddListener(OMEnter);
        trigger.triggers.Add(entry2);

        entry3.callback = new EventTrigger.TriggerEvent();
        entry3.callback.AddListener(OMExit);
        trigger.triggers.Add(entry3);
    }

}
