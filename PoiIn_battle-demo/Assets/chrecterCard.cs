using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON2;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using LitJson;
public class chrecterCard : MonoBehaviour
{
    //该角色的名字
    public string chracterName;
    //该角色的属性值
    public Dictionary<string, int> status;
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
        public string name;
        public string description;
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


    
    
}
