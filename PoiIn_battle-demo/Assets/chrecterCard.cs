﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON2;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using LitJson;
using System.Drawing;
using UnityEditor;

public class chrecterCard : MonoBehaviour
{
    //该角色的名字
    public string chracterName;
    //该角色的属性值
    public Dictionary<string, int> status;
    public List<Sprite> Sprites = new List<Sprite>();
    private Image image;
    //法术的结构体
    [Serializable]
    public struct spells
    {
        public string spellName;
        public string description;
        public Dictionary<string, int> Launching_Conditions;
    }
    //所有法术的链表
    public List<spells> spelleList = new List<spells>();
   //中间变量
    private spells mySpells;
    //背包物品（持ち物）结构体
    public struct motimono
    {
        //持有物名称
        public string name;
        //持有物说明
        public string description;
        //持有物图标sprite
        public Sprite icon;
    }
    //背包物品链表
    public List<motimono> Motimonos;
    
    private void Start()
    {
        status = new Dictionary<string, int>();
        mySpells = new spells();
        Motimonos = new List<motimono>();
        loadJson();
    }

    public void loadJson()
    {
        JsonData jd = new JsonData();
        //填入人物卡所在的文件夹位置
        jd = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/chracter/mixieerBulang.json"));
        
        chracterName = jd["name"].ToString();
        
        JsonData jdItem = new JsonData();
        jdItem = jd["spells"];
        foreach (JsonData data in jdItem)
        {
            mySpells = new spells();
            mySpells.Launching_Conditions = new Dictionary<string, int>();
            mySpells.spellName = data["name"].ToString();
            mySpells.description = data["description"].ToString();
            foreach (JsonData jsonData in data["Launching_conditions"])
            {
                mySpells.Launching_Conditions.Add(StatusParse(jsonData.ToString(),cmd.status),
                                                                    Convert.ToInt32(StatusParse(jsonData.ToString(),cmd.value)));
            }
            spelleList.Add(mySpells);
        }

        motimono myMotimono = new motimono();
        jdItem = new JsonData();
        jdItem = jd["motimono"];

        foreach (JsonData jsonData in jdItem)
        {
            myMotimono = new motimono();
            myMotimono.name = jsonData["name"].ToString();
            myMotimono.description = jsonData["description"].ToString();
            //Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(Application.dataPath+ "/testBackIcon/" + myMotimono.name + ".png");
            //myMotimono.icon = Resources.Load<Sprite>(myMotimono.name + ".png");
            myMotimono.icon = LoadByIO(myMotimono.name);
            Sprites.Add(myMotimono.icon);
            Motimonos.Add(myMotimono);
        }
        
        
    }
    public enum cmd
    {
        status,
        procativity,
        value
    }
 
    private string StatusParse(string str,cmd _cmd)
    {
        string[] s = str.Split(':');
        switch (_cmd)
        {
            case cmd.status:
                return s[0];
                break;
            case cmd.procativity:
                return s[1];
                break;
            case cmd.value:
                if (s[1] == "<")
                {
                    s[2] = "-" + s[2];
                }
                return s[2];
            break;
            default:
                return null;
        }
        
    }
    /// <summary>
    /// 以IO方式进行加载
    /// </summary>
    private Sprite LoadByIO(string iconName)
    {
        //创建文件读取流
        FileStream fileStream = new FileStream(Application.dataPath+ "/testBackIcon/" + iconName + ".png", FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);
        //释放文件读取流
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;
        //创建Texture
        int width = 80;
        int height = 80;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);

        //创建Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
    /// <summary>
    /// 获得物品信息链表
    /// </summary>
    /// <returns></returns>
    public List<motimono> GETMotimonos()
    {
        return Motimonos;
    }
}
