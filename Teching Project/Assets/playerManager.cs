using UnityEngine;

public class playerManager : MonoBehaviour
{
    public bool confirm;
    public GameObject canvas;
    private timeLimitation Canvas;
    public int passedSeconds = 5;
    void Start()
    {
        Canvas = canvas.GetComponent<timeLimitation>();
    }
    private void Update()
    {
        if (confirm == true)
        {
            Canvas.timePass(passedSeconds);
            confirm = false;
        }
    }
}
