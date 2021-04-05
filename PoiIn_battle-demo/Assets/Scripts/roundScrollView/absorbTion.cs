using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class absorbTion : MonoBehaviour
{
    private roundScrollView _roundScrollView;
    private Vector3 target2Dposition;
    void Start()
    {
        _roundScrollView = FindObjectOfType<roundScrollView>();
        target2Dposition = _roundScrollView.judgeAbsorb.transform.position;
    }

    void Update()
    {
        if(_roundScrollView.canAbsorb){
            // collide = true;
            StartCoroutine(
                lerpMove(
                    this.gameObject,
                    target2Dposition,
                    0.1f
                )
            );
        }
    }

    // public bool collide;
    private void OnCollisionEnter2D(Collision2D other) {

    }
    private void OnCollisionExit2D(Collision2D other) {
        
    }

    IEnumerator lerpMove(GameObject gameObj, Vector3 target2Dposition, float lerpFactor)
    {
        gameObj.transform.position = Vector3.Lerp(gameObj.transform.position, target2Dposition, lerpFactor);
        if (Vector3.SqrMagnitude(target2Dposition -  gameObj.transform.position)<1f)
        {
            gameObj.transform.position = target2Dposition;
            yield return 0;
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(lerpMove(gameObj, target2Dposition, lerpFactor));
        }
        yield return 0;
    }
}
