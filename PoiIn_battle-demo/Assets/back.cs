using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back : MonoBehaviour
{
    public GameObject[] board;
    public bool active1;
    public bool active2;
    public bool[] boolArray = new bool[100];
    private void Start()
    {
        board = GameObject.FindGameObjectsWithTag("board");
        foreach (GameObject o in board)
        {
            o.transform.position = new Vector3(0,1,-4);
            o.SetActive(false);
            //使用C#封装好的自动遍历，性能优良且可读性好
        }
        //board[0].transform.position = new Vector3(0, 1, -4);
        //board[0].SetActive(false);
        active1 = false;
        //board[1].transform.position = new Vector3(0, 1, -4);
        //board[1].SetActive(false);
        active2 = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Tab))
        {
            if (active1 == true)
            {
                board[0].SetActive(false);
                active1 = false;
            }
            else
            {
                board[0].SetActive(true);
                active1 = true;
            }
        } 
        if (Input.GetKeyDown (KeyCode.LeftShift))
        {
            if (active2 == true)
            {
                board[1].SetActive(false);
                active2 = false;
            }
            else
            {
                board[1].SetActive(true);
                active2 = true;
            }
        } 
    }
}
