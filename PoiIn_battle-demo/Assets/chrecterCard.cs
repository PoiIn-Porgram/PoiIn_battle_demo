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
    public string chracterName;
    public Dictionary<string, int> status;
    [Serializable]
    public struct spells
    {
        public string spellName;
        public string description;
        public Dictionary<string, int> Launching_Conditions;
    }
    
    public List<spells> spelleList = new List<spells>();
   
    public spells mySpells;

    private void Start()
    {
        status = new Dictionary<string, int>();
        mySpells = new spells();

        loadJson();
    }

    public void loadJson()
    {
        JsonData jd = new JsonData();
        jd = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/chracter/Michele_Bran.json"));
        
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
