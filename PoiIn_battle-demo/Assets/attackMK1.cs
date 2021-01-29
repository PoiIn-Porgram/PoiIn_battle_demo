using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackMK1 : MonoBehaviour
{
    private GameObject _particleSystem4Attack;
    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem4Attack = GameObject.Find("ParticleSystem4Attack");//待优化，将粒子系统丢入resource
        _particleSystem = _particleSystem4Attack.GetComponent<ParticleSystem>();
    }
    
    public float attack(GameObject character){
        Vector3 pos = character.transform.position;
        float health = 100f;//改角色生命值
        _particleSystem4Attack.transform.position = new Vector3
            (character.transform.position.x,
             character.transform.position.y,
             character.transform.position.z
            );
        _particleSystem.Play();
        return health;
    }
}
