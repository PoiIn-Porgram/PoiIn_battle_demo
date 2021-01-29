using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class newCharacterCard : MonoBehaviour
{
    [FormerlySerializedAs("character_name")] 
    public string characterName;
    //  角色名
    public Dictionary<string,int> status;
    //  角色属性字典，由于属性值种类不变，直接用键值对取值
    
    public struct state{
        public string name;
        //  状态名
        public int rest_round;
        //  状态剩余回合，若持续状态则为-1
        public string stateCode;
        //  状态效果码
    }
    public List<state> stateList;
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
    public List<weapon> weaponList;
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
    public List<skill> skillList;
    //  技能的列表
}
