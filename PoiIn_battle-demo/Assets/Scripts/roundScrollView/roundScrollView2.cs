using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class roundScrollView2 : MonoBehaviour
{
    Vector3 originposition;
    private GameObject Sphere2Spin;
    // private Rigidbody2D rigidbody;
    private float speed = 0;
    private float scale = 1;
    void Start()
    {
        Sphere2Spin = this.gameObject;
        StartListeners(Sphere2Spin);
    }

    void Update()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.UpArrow)) && canControl)
        //与Scroll1相反
        {
            Sphere2Spin.transform.Rotate(new Vector3(0,0,90), 10f);
            // Sphere2Spin.transform.rotation = Sphere2Spin.transform.rotation + Quaternion.Euler(0,5f,0);
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.DownArrow)) && canControl)
        {
            Sphere2Spin.transform.Rotate(new Vector3(0,0,-90), 10f);
        }else{

        }

    }

    // IEnumerator lerpMove(GameObject gameObj, Quaternion target2Drotation, float lerpFactor)
    // {
    //     gameObj.transform.rotation = Quaternion.Euler(Vector3.Lerp(gameObj.transform.rotation.eulerAngles, target2Drotation.eulerAngles, lerpFactor));
    //     if (Vector3.SqrMagnitude(target2Drotation.eulerAngles -  gameObj.transform.rotation.eulerAngles)<1f)
    //     {
    //         gameObj.transform.rotation = target2Drotation;
    //         yield return 0;
    //     }
    //     else
    //     {
    //         yield return new WaitForSeconds(0.02f);
    //         StartCoroutine(lerpMove(gameObj, target2Drotation, lerpFactor));
    //     }
    //     yield return 0;
    // }

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
