using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSpawner : MonoBehaviour
{
    public List<GameObject> characterList;
    public Vector3 getChracter2Dposition(Vector3 _3Dposition)
    {
        return new Vector3((_3Dposition.x-_3Dposition.z)*0.3f,
            (_3Dposition.z+_3Dposition.x)*0.15f,
            _3Dposition.z+_3Dposition.x);
    }

    public void spawnerCharacter(string characterName,Vector3Int characterAbstractPosition)
    {
        foreach (GameObject o in characterList)
        {
            if (o.name ==characterName)
            {
                GameObject gameObject = Instantiate(o);
                gameObject.transform.position =
                    getChracter2Dposition(characterAbstractPosition) + new Vector3(0, 0.34f, 0);
                gameObject.transform.SetParent(GameObject.FindWithTag("chessBoard").transform);
            }
        }
    }
}