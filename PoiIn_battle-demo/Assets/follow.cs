using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {
    [SerializeField]
    Camera UI_Camera;
    // [SerializeField]
    public RectTransform btn1;
    public RectTransform btn2;
    public RectTransform btn3;
    public RectTransform btn4;
    public RectTransform btn5;
    public RectTransform btn6;

    [SerializeField]
    GameObject obj;
    [SerializeField]
    Canvas UI_Canvas;
    // Update is called once per frame
    void Update () {
        UpdatePosition();
    }
    /// <summary>
    /// 更新button位置
    /// </summary>
    void UpdatePosition()
    {
        Vector2 mouseDown = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(UI_Canvas.transform as RectTransform, mouseDown, UI_Camera, out mouseUGUIPos);
        if (isRect)
        {
            btn1.anchoredPosition = mouseUGUIPos + new Vector2(-100, 15);
            btn2.anchoredPosition = mouseUGUIPos + new Vector2(-100, -15);
            btn3.anchoredPosition = mouseUGUIPos + new Vector2(0, 50);
            btn4.anchoredPosition = mouseUGUIPos + new Vector2(0, -50);
            btn5.anchoredPosition = mouseUGUIPos + new Vector2(100, 15);
            btn6.anchoredPosition = mouseUGUIPos + new Vector2(100, -15);

        }
    }
}