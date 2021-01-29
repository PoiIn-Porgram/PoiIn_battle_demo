using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using LitJson;
using System.Drawing;
using System.Text;
using MiniJSON2;
using UnityEditor;

public class newCharacterCard : MonoBehaviour
{
    [FormerlySerializedAs("character_name")] 
    public string characterName;
    //  角色名
    //public Dictionary<string,int> status = new Dictionary<string, int>();
    //  角色属性字典，由于属性值种类不变，直接用键值对取值
    
    public struct state{
        public string name;
        //  状态名
        public int rest_round;
        //  状态剩余回合，若持续状态则为-1
        public string stateCode;
        //  状态效果码
    }
    public List<state> stateList = new List<state>();
    //  状态列表

    public struct weapon{
        public string name;
        //  武器名称
        public string type;
        // 武器种类
        public string weapon_class;
        // 武器阶级(初始，进阶，终极)
        public string description;
        // 武器简介
        public Vector3Int[] attack_area;
        //  武器攻击范围
        public int weapon_power;
        //  武器威力
        public string weaponCode; 
        //  武器特殊效果码
    }
    public List<weapon> weaponList = new List<weapon>();
    //持有的武器列表

    public struct skill{
        public bool isLeagal;
        //  该技能是否能发动
        public string name;
        //  技能名称
        public string skill_consume;
        //  技能消耗的属性值
        public string[] skill_target;
        //  技能的目标
        public string skill_effect;
        //  技能的效果码
        public string acquire_condition;
        //  技能的习得条件
    }
    public List<skill> skillList = new List<skill>();
    //  技能的列表


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public bool isNewGame = true;
    private JsonData jd = new JsonData();
    private state curState = new state();
    private  weapon curWeapon = new weapon();
    private skill curSkill = new skill();
    private chracterMove _chracterMove;
    private testMap _testMap;
    private void Start()
    {
        characterName = gameObject.name;
        _chracterMove = GetComponent<chracterMove>();
        _testMap = FindObjectOfType<testMap>();
        _loadJson();
        //loadStatus();
        //loadState();
        //loadWeapons();
        //loadSkills();
    }

     private void loadJson()
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
        jd = new JsonData();
        
        //characterName = jd["name"].ToString();
    }


     private void loadState()
     {
         
     }

     private void loadWeapons()
     {
         JsonData jsonData = new JsonData();
         jsonData = jd["Weapons"];
         foreach (JsonData data in jsonData)
         {
             curWeapon = new weapon();
             curWeapon.name = data["name"].ToString();
             curWeapon.description = data["description"].ToString();
             curWeapon.type = data["type"].ToString();
             curWeapon.weapon_class = data["class"].ToString();
             weaponList.Add(curWeapon);
         }
     }

     private void loadSkills()
     {
         JsonData jsonData = new JsonData();
         jsonData = jd["Skills"];
         foreach (JsonData data in jsonData)
         {
             curSkill = new skill();
             curSkill.name = data["name"].ToString();
         }
     }

     private List<Vector3Int> getNabourVector3Int(Vector2Int area)
     {
         Vector3Int thisPosition = _chracterMove.thisPosition;
         List<Vector3Int> returnMap = new List<Vector3Int>();
         for (int i = -area.y; i <= area.y; i++)
         {
             for (int j = -area.y; j <= area.y; j++)
             {
                 Vector3Int newVector3Int = new Vector3Int(thisPosition.x + i, thisPosition.y, thisPosition.z + j);
                 if (_testMap.savedBlocks.ContainsKey(newVector3Int))
                 {
                     returnMap.Add(newVector3Int);
                 }
             }
         }

         for (int i = -area.x; i <= area.x; i++)
         {
             for (int j = -area.x; j <= area.x; j++)
             {
                 Vector3Int newVector3Int = new Vector3Int(thisPosition.x + i, thisPosition.y, thisPosition.z + j);
                 if (returnMap.Contains(newVector3Int))
                 {
                     returnMap.Remove(newVector3Int);
                 }
             }
         }

         return returnMap;
     }
     
     
     
     
     
      public void _loadJson()
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
        JsonData jsonData = new JsonData();
        jsonData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/LekkerVerberens.json"));
    }
}
