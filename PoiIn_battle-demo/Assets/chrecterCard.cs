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
    public string name;
    public List<string> TestLcList;
    public Dictionary<string, int> status;
    public string[] testLC;
    public struct spells
    {
        public Dictionary<string, string> description ;
        public Dictionary<string, List<string>> Launching_Conditions;

        public string getDescription(string name)
        {
            return description["name"];
        }
    }
    public spells mySpells;

    private void Start()
    {
        status = new Dictionary<string, int>();
        mySpells = new spells();
        mySpells.description = new Dictionary<string, string>();
        mySpells.Launching_Conditions = new Dictionary<string, List<string>>();
        loadJson();
    }

    public void loadJson()
    {
        JsonData jd = new JsonData();
        jd = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/chracter/Michele_Bran.json"));
        
        name = jd["name"].ToString();
        
        JsonData jdItem = new JsonData();
        jdItem = jd["spells"];
        foreach (JsonData data in jdItem)
        {
            List<string> curList = new List<string>();
            foreach (JsonData jsonData in data["Launching_conditions"])
            {
                TestLcList.Add(jsonData.ToString());
            }
        }
    }

}
