using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackMK2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private float time;
    private Vector3 speed;
    private Vector3 Gravity;
    private Vector3 currentAngle;
    public float dTime = 0;//public目的在于万一出bug可以按攻击键时恢复

    public void attack(GameObject character, GameObject projectile, Vector3 targetPos, float shotSpeed)//projectile后期换prefab
    {
        float g = -10;
        // GameObject projectile;
        // Transform emitPosition = character.transform;
        GameObject toShoot = Instantiate(projectile);
        Debug.Log(toShoot);
        toShoot.transform.position = character.transform.position;//发射位置未必是角色位置
        // Vector3 pos = character.transform.position;
        Vector3 posToShoot = toShoot.transform.position;
        time = Vector3.Distance(posToShoot, targetPos)/shotSpeed;
        speed = new Vector3(
            (targetPos.x - posToShoot.x) / time,
            (targetPos.y - posToShoot.y) / time - 0.5f * g * time, 
            (targetPos.z - posToShoot.z) / time
        );
        Gravity = Vector3.zero;
        StartCoroutine(paraCurveMove(toShoot, speed, targetPos, g));
    }

    private IEnumerator paraCurveMove(GameObject projectile, Vector3 speed, Vector3 targetPos, float g)
    {

        Gravity.y = g * dTime;
        projectile.transform.position += (speed + Gravity) * 0.02f;//根据waitforseconds参数调节
        // currentAngle.x = -Mathf.Atan((speed.y + Gravity.y) / speed.z) * Mathf.Rad2Deg;
        // projectile.transform.eulerAngles = currentAngle;

        if (Vector3.SqrMagnitude(targetPos -  projectile.transform.position)<0.1f)
        {
            projectile.transform.position = targetPos;
            Destroy(projectile);
            // dTime = 0;
            //伤害函数写这儿
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            dTime += 0.02f;
            StartCoroutine(paraCurveMove(projectile, speed, targetPos, g));
        }
        yield return 0;
    }
}
