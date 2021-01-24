﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    private enemyStatus _enemyStatus;

    private void Start()
    {
        _enemyStatus = GetComponent<enemyStatus>();
        foreach (enemyStatus._enemyStatus enemy in _enemyStatus.enemies)
        {
            switch (enemy.Type)
            {
                case enemyStatus._enemyStatus.enemyType.Type_A:
                    Instantiate(_enemyStatus.enemyList[0]).transform.position =
                        getChracter2Dposition(enemy.statrPosition) + new Vector3(0, 0.34f, 0);
                        //enemy.statrPosition + new Vector3Int(0, 1, 0);
                    break;
                case enemyStatus._enemyStatus.enemyType.Type_B:
                    break;
                case enemyStatus._enemyStatus.enemyType.Type_C:
                    break;
            }
        }
    }
    public Vector3 getChracter2Dposition(Vector3 _3Dposition)
    {
        return new Vector3((_3Dposition.x-_3Dposition.z)*0.3f,
            (_3Dposition.z+_3Dposition.x)*0.15f,
            _3Dposition.z+_3Dposition.x);
    }
}

