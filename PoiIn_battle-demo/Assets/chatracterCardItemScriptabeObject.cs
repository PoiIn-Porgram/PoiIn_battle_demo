using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string characterName;
    public Sprite[] standingPicture;
    public int HP, SP, AP, PRA, ACA, AGI;
    [TextArea]
    public string characterInfo;
    public List<weapon> weapons;
    public List<skill> skills;
    public List<speciality> Specialities;
    public List<motimono> Motimonos;
}

public enum weaponType
{
        huntingGun,
        sword,
        tinyKnife
}

public enum weaponClass
{
    initialWeapon,
    progressiveWeapon,
    finalWeapon
}

[Serializable]
public class weapon
{

    public string weaponName;
    public weaponType Type;
    public weaponClass Class;
    [TextArea]
    public string description;
    public Vector2Int attackArea;
    public int power;
    public int SP_consume;
    [TextArea]
    public string PoiInCodeForWeapon;
    public Sprite[] icon;
    
}

public enum skillTarget
{
    friend,
    enemy,
    friends,
    enemies
}



[Serializable]
public class skillLevel
{
    public int SkillLevel;
    public int PRA_upgradeToThisLevel_threshold;
    public string level_upgradeToThisLevel_consume;
    public skillTarget Target;
    public Vector2Int castArea;
    [TextArea]
    public string PoiInCodeForSkill;
    public int skillCD;
    public Sprite[] icon;
}

[Serializable]
public class skill
{
    public string skillName;
    [TextArea]
    public string description;

    public List<skillLevel> Levels;

}


[Serializable]
public class speciality
{
    
}

[Serializable]
public class motimono
{
    
}