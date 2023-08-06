using UnityEngine;

public class TimeScaleHelper : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetKey(KeyCode.T)) return;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale += 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale -= 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Time.timeScale = 1f;
        }
    }
}