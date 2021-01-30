using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackMK1 : MonoBehaviour
{
    //老particle模式
    // private GameObject _particleSystem4Attack;
    // private ParticleSystem _particleSystem;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     _particleSystem4Attack = GameObject.Find("ParticleSystem4Attack");//待优化，将粒子系统丢入resource
    //     _particleSystem = _particleSystem4Attack.GetComponent<ParticleSystem>();
    // }
    
    // public float attack(GameObject character){
    //     Vector3 pos = character.transform.position;
    //     float health = 100f;//改角色生命值
    //     _particleSystem4Attack.transform.position = new Vector3
    //         (character.transform.position.x,
    //          character.transform.position.y,
    //          character.transform.position.z
    //         );
    //     _particleSystem.Play();
    //     return health;
    // }

    

    //2.0直接GameObject模式

    private GameObject _player;//暂时没用

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("mixieerBulang");
    }

    public void attack(GameObject character, GameObject projectile, Vector3 targetPos, float moveSpeed)//projectile后期换prefab
    {
        // GameObject projectile;
        // Transform emitPosition = character.transform;
        GameObject toShoot = Instantiate(projectile);
        toShoot.transform.position = character.transform.position;
        // Vector3 pos = character.transform.position;
        StartCoroutine(straightMove(toShoot, targetPos, moveSpeed));
    }

    //平移函数，攻击不用加速度了，迭代器真好用
    private IEnumerator straightMove(GameObject projectile, Vector3 targetPos, float moveSpeed) //moveSpeed==target即一步到位
    //改waitforseconds的参数可以改帧率，可以作为画质选项使用
    {
        Vector3 pos = projectile.transform.position;
        projectile.transform.position = Vector3.MoveTowards(pos, targetPos, moveSpeed);
        if (Vector3.SqrMagnitude(targetPos -  projectile.transform.position)<0.005f)
        {
            // projectile.transform.position = targetPos;//可以不要
            Destroy(projectile);
            //此处加伤害函数
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(straightMove(projectile, targetPos, moveSpeed));
        }
        yield return 0;
    }
}
