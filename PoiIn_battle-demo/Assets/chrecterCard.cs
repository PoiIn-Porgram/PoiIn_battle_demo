using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON2;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using LitJson;
using System.Drawing;
using System.Text;
using UnityEditor;

public class chrecterCard : MonoBehaviour
{
    public bool isNewGame = true;
    //该角色的名字
    public string chracterName;//start里赋值
    //该角色的属性值
    public Dictionary<string, int> status = new Dictionary<string, int>();
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
        chracterName = "mixieerBulang";
        status = new Dictionary<string, int>();
        mySpells = new spells();
        Motimonos = new List<motimono>();
        loadJson();
        loadStatus();
        Debug.Log(jd["name"].ToString());
    }
    private JsonData jd = new JsonData();
    public void loadJson()
    {
        string folder = "original/";
        if (isNewGame)
        {
            folder = "original/";
            isNewGame = false;
        }
        else
        {
            folder = "savedData/";
        }
        //填入人物卡所在的文件夹位置
        string[] notCloneName = gameObject.name.Split('(');

        jd = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/chracter/"+folder+notCloneName[0]+".json"));
        
        jd["isNewGame"] = false;//存档标记
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

    public int damegeDeterminationParser(int dice,string skillName)
    {
        int damage = 0;
        int factor = 1;
        JsonData jsonData = jd[skillName];
        string damageFormula = jsonData["damage_determiniation"].ToString();
        string[] damageSection = damageFormula.Split('$');
        foreach (string str in damageSection)
        {
            if (str[0] == '-')
            {
                factor = -1;
            }
            else
            {
                factor = 1;
            }
            string instruction = str.Substring(1, str.Length - 1);

            switch (instruction)
            {
                case "Dice":
                    damage += (dice * factor);
                    break;
                case "Proficiency":
                    int proficiency = Convert.ToInt32(jsonData["proficiency"].ToString());
                    damage += (proficiency*factor);
                    break;
                default:
                    Debug.Log(instruction);
                    damage += status[instruction] * factor;
                    break;
            }
        }
        return damage;
    }
    public void loadStatus()
    {
        status = new Dictionary<string, int>();
        JsonData jsonData = new JsonData();
        jsonData = jd["status"];
        status.Add("HP",Convert.ToInt32(jsonData["HP"].ToString()));
        status.Add("SP",Convert.ToInt32(jsonData["SP"].ToString()));
        status.Add("PL",Convert.ToInt32(jsonData["PL"].ToString()));
        status.Add("AC",Convert.ToInt32(jsonData["AC"].ToString()));
        status.Add("SL",Convert.ToInt32(jsonData["SL"].ToString()));
        status.Add("AP",Convert.ToInt32(jsonData["AP"].ToString()));
    }

    public void changeStatus()
    {
        //status[statusToChange.Key] = statusToChange.Value;
        jd["status"]["HP"] = 10;
        JsonWriter jsonWriter = new JsonWriter();
        
        Debug.Log(jd["status"]["HP"]);
        string folder;
        if (isNewGame)
        {
            folder = "original/";
            isNewGame = false;
        }
        else
        {
            folder = "savedData/";
        }
        File.WriteAllText(Application.dataPath+"/chracter/"+folder+gameObject.name+".json",
            Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(JsonMapper.ToJson(jd))),
            Encoding.ASCII);
        Debug.Log("newJson");
    }
    
}
