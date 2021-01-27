using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// </summary>



public class damageCalculations : MonoBehaviour
{
    
    public int damegeDeterminationParser(int dice, string skillName)
    {
        int damage = 0;
        int factor = 1;
        JsonData jsonData = jd[skillName];
        string damageFormula = jsonData["damage_determiniation"].ToString();
        string[] damageSection = damageFormula.Split('$');
        foreach (string str in damageSection)
        {
            if (str[0] == '-')
            {
                factor = -1;
            }
            else
            {
                factor = 1;
            }
            string instruction = str.Substring(1, str.Length - 1);

            switch (instruction)
            {
                case "Dice":
                    damage += (dice * factor);
                    break;
                case "Proficiency":
                    int proficiency = Convert.ToInt32(jsonData["proficiency"].ToString());
                    damage += (proficiency * factor);
                    break;
                default:
                    Debug.Log(instruction);
                    damage += status[instruction] * factor;
                    break;
            }
        }
        return damage;
    }
    void Start()
    {
        private int weaponPower = 20;//Ó²±àÂë
    private int defence = 27;
    private int intelligence = 38;
    string str = "Ãé×¼¹¥»÷";



    private int finalDamageMultiplier = 0;//×îÖÕÉËº¦
    private int a = Random.Range(1, 101);
    private int b = Random.Range(1, 101);
    private int damageOfEnemy = 0;

    damageOfEnemy = intelligence + defence-b£»

        private int damage = damegeDeterminationParser(a, str);


        if (damege<damageOfEnemy)
        {
               finalDamageMultiplier=0;
        

        }
        else
{

    finalDamageMultiplier = weaponPower +£¨int£©£¨£¨damege - damageOfEnemy£©/ 10£©;
}

    }


}
