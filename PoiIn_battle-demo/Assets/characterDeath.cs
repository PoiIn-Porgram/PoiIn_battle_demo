using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterDeath : MonoBehaviour
{
    private GameObject _particleSystem4Death;
    private ParticleSystem _particleSystem;

    // private ParticleSystem.ShapeModule _editableShape;

    // Start is called before the first frame update
    void Start()
    {
        target2Drotation = Quaternion.Euler(new Vector3(0, 0, -60f));
        _particleSystem4Death = GameObject.FindGameObjectWithTag("particleSystem");
        _particleSystem = _particleSystem4Death.GetComponent<ParticleSystem>();
        // _editableShape = _particleSystem.shape;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float lerpFactor = 0.1f;
    private Quaternion target2Drotation;
    IEnumerator lerpRotate()
    {
        Vector3 thisRotationVector = this.transform.rotation.eulerAngles;
        Vector3 targetRotationVector = target2Drotation.eulerAngles;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, target2Drotation, lerpFactor);
        if (Vector3.SqrMagnitude(targetRotationVector -  thisRotationVector)<0.005f)
        {
            this.transform.rotation = target2Drotation;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(lerpRotate());
        }
        yield return 0;
    }


    public void setDead(GameObject character){
        Destroy(character);
    }

    public void kill(){
        StartCoroutine(lerpRotate());
        //倒地，目前只是一个旋转，感觉还是用美术资源更好
        // _editableShape.position = this.gameObject.transform.position;
        _particleSystem4Death.transform.position = new Vector3
            (this.transform.position.x,
             this.transform.position.y,
             -2
            );
        _particleSystem.Play();
        //加一个渐隐效果,插值算法不易维护打算用animator
        //需要之前的定时器函数
        //setDead(this.gameObject);
    }
}
