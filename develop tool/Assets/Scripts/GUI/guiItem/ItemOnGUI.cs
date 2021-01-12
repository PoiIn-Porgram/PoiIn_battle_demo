using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOnGUI : MonoBehaviour {

    public ItemOnGUIStyle.itemonGUIstyle itemonguiStyle;
    [HideInInspector]
    public Image image;
    [HideInInspector]
    public Text item_num;
    void Start()
    {
        image = GetComponent<Image>();
        item_num = GetComponentInChildren<Text>();
        if (itemonguiStyle==ItemOnGUIStyle.itemonGUIstyle.gui_EMPTY)
        {
            image.sprite = null;
            image.enabled = false;
            item_num.gameObject.SetActive(false);
            item_num.text = "";
        }
    }
}
