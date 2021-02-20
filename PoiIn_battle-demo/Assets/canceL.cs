using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class canceL : MonoBehaviour
{
    public GameObject canvas;
    private void OMClick(BaseEventData pointData){
        omClick();
    }

    private void omClick(){
        canvas.SetActive(false);
    }
    //btn自带，不需要这个脚本，暂时无用

}
