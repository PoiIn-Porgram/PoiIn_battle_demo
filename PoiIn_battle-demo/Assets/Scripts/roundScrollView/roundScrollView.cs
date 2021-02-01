using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class roundScrollView : MonoBehaviour
{
    Vector3 originposition;
    private GameObject followSphere;
    private Rigidbody2D rigidbody;
    Vector3 autoMove;
    private float speed = 0;
    private float scale = 1;

    void Start()
    {
        followSphere = GameObject.Find("sphereToMoveAround");
        rigidbody = followSphere.GetComponent<Rigidbody2D>();
        originposition = GameObject.Find("stationarySphere").transform.position;
        StartListeners(this.gameObject);
    }


    void Update()
    {

        //滚轮控制部分
        if ((Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.UpArrow)) && canControl)
        //小于0是往上运转；
        {
            speed = 100f;
            scale = 1;
            rigidbody.velocity = new Vector2(0,0);
            autoMove = AutoMove(originposition, followSphere.transform.position, Vector3.forward, 90f);
            rigidbody.velocity = rigidbody.velocity +
                                (Vector2)(
                                    (originposition - followSphere.transform.position).normalized * 25f //即使中心圆移动也依旧跟随中心圆旋转
                                    + autoMove * speed //50f //向心加速度
                                );

        }
        else if ((Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.DownArrow)) && canControl)
        {
            speed = -100f;
            scale = 1;
            rigidbody.velocity = new Vector2(0,0);
            autoMove = AutoMove(originposition, followSphere.transform.position, Vector3.forward, 90f);
            rigidbody.velocity = rigidbody.velocity +
                                (Vector2)(
                                    (originposition - followSphere.transform.position).normalized * 25f //即使中心圆移动也依旧跟随中心圆旋转
                                    + autoMove * speed //50f //向心加速度
                                );
        }else{
            speed = 0;
            scale = 0.99f;
            autoMove = AutoMove(originposition, followSphere.transform.position, Vector3.forward, 90f);
            rigidbody.velocity = rigidbody.velocity +
                                (Vector2)(
                                    (originposition - followSphere.transform.position).normalized * 25f //即使中心圆移动也依旧跟随中心圆旋转
                                    + autoMove * speed //50f //向心加速度
                                );
            rigidbody.velocity *= scale;
        }

        //圆周运动部分

        rigidbody.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));//有些抖动残影呢，先这样再说
        //可以把显示图片的部分单独拎出来做一个GameObject，跟随小圆坐标位置，应该就不抖了

    }
    //心情好再用迭代器优化下试试

    private Vector3 AutoMove(Vector3 position, Vector3 center, Vector3 axis, float angle){
        Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
        Vector3 resultVector = center + point;
        return (resultVector - center).normalized; //向心方向向量
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
    }

    private void omExit(){
        canControl = false;
    }
}
