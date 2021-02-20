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
    public Vector3Int abstractPosition;//方便调试与调用
    private GameObject _player;
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
    private void omEnter(){
        Debug.Log("MouseHit!");
        moveUp(); //有bug待修复
    }

    private void OMExit(BaseEventData pointData) {
        omExit();
    }
    private void omExit(){
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


    private void omClick(){
        Debug.Log("MouseClicked!");
        //闪现走法
        // _player.transform.position = new Vector3(thisBlock.transform.position.x,
        //     thisBlock.transform.position.y + 0.34f,
        //     thisBlock.transform.position.z
        //     // _player.transform.position.z
        // );
        target2Dposition = originalPosition + new Vector3(0, 0.34f, -1);
        //-1代表人物位置
        //置前地砖一格
        //之后可根据战局规划做修改
        _cubeLerp.target2Dposition = target2Dposition;
        StartCoroutine(
        _cubeLerp.cubeLerpMove(_player,
            0.1f
            )
        );
        _player.GetComponent<chracterMove>().formerposition = target2Dposition;
        _player.GetComponent<chracterMove>().thisPosition = thisBlock.GetComponent<cubeController>().abstractPosition;
        //可能可以不用getcomponent？没试过，优化时这里是个入手点，记着
        Debug.Log(_player.GetComponent<chracterMove>().thisPosition);
    }

    public void ifGround(){
        //不同砖块不同效果预留接口
    }

    public void ifEnemy(){

    }
    
    private Vector3 target2Dposition4Cube;
    //为了和角色的2Dposition区分开而特加此变量
    //后续可优化此变量
    //选择1：将此脚本中lerpMove（专为地砖设计）给到cubeLerp.cs中，并与角色position共用一个变量，条例不清晰但省资源
    //选择2：为此脚本中关于地砖的代码段新开一个脚本，与控制点击移动的脚本分开，条例清晰但几乎没优化。

    public void moveUp(){
        target2Dposition4Cube = originalPosition + new Vector3(0, 0.1f, 0);
        StartCoroutine(
        lerpMove(thisBlock,
            0.1f
            )
        );
        collider2D.size = new Vector2(0.3f,0.5f);
        // collider2D.offset = new Vector2(0f,-0.4f);
    }

    public void moveDown(){
        target2Dposition4Cube = originalPosition;
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
        gameObj.transform.position = Vector3.Lerp(gameObj.transform.position, target2Dposition4Cube, lerpFactor);
        if (Vector3.SqrMagnitude(target2Dposition4Cube -  gameObj.transform.position)<0.005f)
        {
            gameObj.transform.position = target2Dposition4Cube;
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
