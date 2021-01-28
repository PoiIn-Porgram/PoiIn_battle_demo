using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventSinario : MonoBehaviour
{
   private enemyStatus _enemyStatus;
   private void Start()
   {

   }

   public void checkSinario(Vector3Int nextPosition)
   {
      foreach (enemyStatus._enemyStatus enemy in _enemyStatus.enemies)
      {
         if (enemy.statrPosition == nextPosition)
         {
            print("get into conflict with " + enemy.Type);
         }
         
      }
   }
}
