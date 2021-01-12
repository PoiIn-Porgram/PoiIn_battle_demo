using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceGenerator : MonoBehaviour
{
    public Transform generatePos;
    

    public GameObject entryPerfab;
    public GameObject entrance;

    // public int timePassed;
    public bool canInit;



    // Start is called before the first frame update
    void Start()
    {
        timePass();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void timePass()
    {
        StartCoroutine(cutDownTime());
    }
    public IEnumerator cutDownTime()
    {
        // timePassed--;

        yield return new WaitForSeconds(3);
        StartCoroutine(cutDownTime());

        if(canInit){
            Init();
            canInit = false;
        }

        yield return null;
    }

    public void Init(){
        entrance = Instantiate(entryPerfab, generatePos.position, Quaternion.identity);
        entrance.GetComponent<Rigidbody2D>().velocity = new Vector2(0,chessBoardManager.speed);
    }
}
