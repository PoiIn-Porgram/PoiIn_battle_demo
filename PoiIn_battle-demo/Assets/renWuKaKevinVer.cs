using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON2;
using System.IO;
using System.Linq;

public class renWuKaKevinVer : MonoBehaviour
{
    public float HP;
    public float MP;
    public int Level;
    public int weaponLevel;
    public int EXP;
    public int EXPMAX;
    public int weaponEXP;
    public int weaponEXPMAX;
    public float ATK;
    public float DEF;

    ///<summary>
    ///力量
    ///</summary>
    public float Str;

    ///<summary>
    ///智力
    ///</summary>
    public float Int;

    ///<summary>
    ///敏捷
    ///</summary>
    public float Agi;

    private /*float*/ string loadJSON(string add){
        JSON toLoad = new JSON();
        string needed = LoadJsonFromFile(ref toLoad, add);
        float converted = Convert.ToSingle(needed);
        return needed;
        // return converted;
    }


    public static string LoadJsonFromFile(ref JSON json, string adress)
	{
		string needed = File.ReadAllText(Application.dataPath + "/" + adress + ".json");
		json.serialized = needed;
        return json.serialized;
	}

    // public void Parse(string str){
    //     str = str.Replace("")
        
    // }

    // void SaveJsonToFile(JSON json, string adress){
    //     File.WriteAllText(Application.dataPath + "/" + adress + ".json", json.serialized, System.Text.Encoding.UTF8)
    // }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
