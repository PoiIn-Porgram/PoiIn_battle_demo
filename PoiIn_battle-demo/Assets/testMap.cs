using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using MiniJSON2;
using System.IO;
using System.Linq;

[System.Serializable]
public class testMap : MonoBehaviour
{
	public Dictionary<Vector3Int, int> savedBlocks = new Dictionary<Vector3Int, int>();
	
	public void LoadData()
	{
		JSON resJson = new JSON();
		
		LoadJsonFromFile(ref resJson);

		GetJsonData(ref resJson, "testMap");

	}
	public void GetJsonData(ref JSON resJson,string key)
	{
		string[] blockLoaded = resJson.ToArray<string>(key);
		foreach (string s in blockLoaded)
		{
			Parse(s);
		}
		Debug.Log("load_success");
	}
	public static void LoadJsonFromFile(ref JSON json)
	{
		string map = File.ReadAllText(Application.dataPath + "/testMap.json");
		json.serialized = map;
	}
	
	public  void Parse(string str)
	{
		str = str.Replace("(", "").Replace(")", "")
			     .Replace("[","").Replace("]","");
		string[] s = str.Split(',');
		Vector3Int newVec = new Vector3Int(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
		
		int style = int.Parse(s[3]);
		
 		savedBlocks.Add(newVec,style);
	}
	void SaveJsonToFile(JSON json)
	{
		
		File.WriteAllText(Application.dataPath + "/testMap.json", json.serialized, System.Text.Encoding.UTF8);
	}
	public void SaveAvatarData()
	{
		JSON resJson = new JSON();
		
		SetJsonData(ref resJson, "testMap");
		
		SaveJsonToFile(resJson);
	}

	private void SetJsonData(ref JSON resJson, string key)
	{
		Dictionary<Vector3Int,int> testBlock = new Dictionary<Vector3Int, int>();

		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				testBlock.Add(new Vector3Int(i,0,j),0);
			}
		}
		resJson[key] = testBlock.ToArray();
	}

	public void initializeTestMap()
	{
		SaveAvatarData();
	}
}

