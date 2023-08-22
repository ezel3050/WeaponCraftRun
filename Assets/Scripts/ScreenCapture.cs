using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScreenCapture : MonoBehaviour
{
    public int resMultiplier = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Capture();
        }

    }
    [Button]

    public void Capture()
    {
       string  name = "SC" + System.DateTime.Now.Date.Year.ToString() + "-" + System.DateTime.Now.Date.Month.ToString()  +"-"+ System.DateTime.Now.Date.Day.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString() + "-" + System.DateTime.Now.Second.ToString();
        UnityEngine.ScreenCapture.CaptureScreenshot("ScreenShots/" +name + ".png", resMultiplier);
    }

}
