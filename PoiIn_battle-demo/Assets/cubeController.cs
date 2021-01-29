using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cubeController : MonoBehaviour
{
    private GameObject thisBlock;
    private CapsuleCollider2D collider2D;
    private Vector3 originalPosition;
    public Vector3 target2Dposition;//必须public且不能封装为lerp的参数
    public Vector3Int abstractPosition;
    private GameObject _player;
    private chracterMove _chracterMove;
    private cubeLerp _cubeLerp;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("mixieerBulang");
        collider2D = this.GetComponent<CapsuleCollider2D>();
        thisBlock = this.gameObject;
        _cubeLerp = FindObjectOfType<cubeLerp>();
        originalPosition = thisBlock.transform.position;
        StartListeners(thisBlock);//鼠标事件监听器
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void StartListeners(GameObject gameObj){
        // EventTrigger trigger = gameObj.GetComponent<EventTrigger>();
        EventTrigger trigger = gameObj.AddComponent<EventTrigger>();
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

    private void OMEnter(BaseEventData pointData) {
        omEnter();
    }
    public void omEnter(){
        Debug.Log("MouseHit!");
        moveUp(); //有bug待修复
    }

    private void OMExit(BaseEventData pointData) {
        omExit();
    }
    public void omExit(){
        Debug.Log("MouseLeave!");
        moveDown();
    }

    private void OMClick(BaseEventData pointData){
        omClick();
        // if(thisBlock.CompareTag("Ground")){
        //     ifGround();
        // }else if(thisBlock.CompareTag("Enemy")){
        //     ifEnemy();
        // }
    }


    public void omClick(){
        Debug.Log("MouseClicked!");
        //闪现走法
        // _player.transform.position = new Vector3(thisBlock.transform.position.x,
        //     thisBlock.transform.position.y + 0.34f,
        //     thisBlock.transform.position.z
        //     // _player.transform.position.z
        // );
        target2Dposition = originalPosition + new Vector3(0, 0.34f, 0);
        _cubeLerp.target2Dposition = target2Dposition;
        StartCoroutine(
        _cubeLerp.lerpMove(_player,
            0.1f
            )
        );
        //依然有点按速度太快漂移bug
        _player.GetComponent<chracterMove>().thisPosition = this.abstractPosition;
    }

    public void ifGround(){
        //不同砖块不同效果预留接口
    }

    public void ifEnemy(){

    }

    public void moveUp(){
        target2Dposition = originalPosition + new Vector3(0, 0.1f, 0);
        StartCoroutine(
        lerpMove(thisBlock,
            0.1f
            )
        );
        collider2D.size = new Vector2(0.3f,0.5f);
        // collider2D.offset = new Vector2(0f,-0.4f);
    }

    public void moveDown(){
        target2Dposition = originalPosition;
        StartCoroutine(
        lerpMove(thisBlock,
            0.1f
            )
        );
        collider2D.size = new Vector2(0.3f,0.3f);
        // collider2D.offset = new Vector2(0f,-0.4f);
    }

    IEnumerator lerpMove(GameObject gameObj, float lerpFactor)
    {
        gameObj.transform.position = Vector3.Lerp(gameObj.transform.position, target2Dposition, lerpFactor);
        if (Vector3.SqrMagnitude(target2Dposition -  gameObj.transform.position)<0.005f)
        {
            gameObj.transform.position = target2Dposition;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(lerpMove(gameObj, lerpFactor));
        }
        yield return 0;
    }
}
