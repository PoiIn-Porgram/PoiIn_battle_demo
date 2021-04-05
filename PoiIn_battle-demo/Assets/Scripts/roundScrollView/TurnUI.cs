using System.Diagnostics;
using System;
// using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class TurnUI : MonoBehaviour
{
    public Transform table;
    public Button btn_turn;

    float turnSpeed = 360;
    float targetAngle = 0;
    float angle = 0;
    float angleBack = 0;
    bool turning = false;
    bool left = false;


    // Start is called before the first frame update
    void Start()
    {
        // btn_turn.onClick.AddListener(Turn);

        StartListeners(this.gameObject);
    }

    void OnDestory()
    {
        // btn_turn.onClick.RemoveListener(Turn);
    }

    void Update(){
        //滚轮控制部分
        if ((Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.UpArrow)) && canControl)
        //小于0是往上运转；
        {   
            if(turning == false){
                print("1");
                Turn(1);
                left = true;
            }
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.DownArrow)) && canControl)
        {   if(turning == false){
                Turn(-1);
                left = false;
            }
        }
        else
        {

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(turning == false) return;
        if(angle >= targetAngle && targetAngle >= angleBack){
            turning = false;
            return;
        }
        angle+=turnSpeed*Time.deltaTime;
        angleBack-=turnSpeed*Time.deltaTime;
        if(left){
            table.Rotate(table.forward, turnSpeed*Time.deltaTime);
        }else{
            table.Rotate(new Vector3 (0,0,-1), turnSpeed*Time.deltaTime);
        }

    }

    // private int m1 = 0;
    private int m = 1;
    void Turn(int direct){
        turning = true;
        // int m = Random.Range(1, 11);
        // int m = 1;
        print(m);
        // if(m>=m1){
        //     m=m-m1+1;
        // }else{
        //     m=m-m1+11;
        // }
        angle = 0;
        angleBack = 0;
        targetAngle = /*360*3*/ direct*36 /*+36*m*/;
        // m += 1;
        if(m==10){
            m=0;
        }else{
            m++;
        }
        // m1 = m;
    }

    private void StartListeners(GameObject gameObj){
        // EventTrigger trigger = gameObj.GetComponent<EventTrigger>();
        EventTrigger trigger = gameObj.AddComponent<EventTrigger>();
        // EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        EventTrigger.Entry entry3 = new EventTrigger.Entry();

        // 鼠标点击事件
        // entry1.eventID = EventTriggerType.PointerClick;
        // 鼠标进入事件 
        entry2.eventID = EventTriggerType.PointerEnter;
        // 鼠标滑出事件 
        entry3.eventID = EventTriggerType.PointerExit;

        // entry1.callback = new EventTrigger.TriggerEvent();
        // entry1.callback.AddListener(OMClick);
        // trigger.triggers.Add(entry1);

        entry2.callback = new EventTrigger.TriggerEvent();
        entry2.callback.AddListener(OMEnter);
        trigger.triggers.Add(entry2);

        entry3.callback = new EventTrigger.TriggerEvent();
        entry3.callback.AddListener(OMExit);
        trigger.triggers.Add(entry3);
    }

    [SerializeField]
    private bool canControl;

    private void OMEnter(BaseEventData pointData){
        omEnter();
    }

    private void OMExit(BaseEventData pointData){
        omExit();
    }

    private void omEnter(){
        canControl = true;
        // print(canControl);
    }

    private void omExit(){
        canControl = false;
    }


}
