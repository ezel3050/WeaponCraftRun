using System;
using UnityEngine;

public class OnCloseListener : MonoBehaviour
{
    public static Action OnClosed;

    private void OnClose()
    {
        PlayerPrefs.SetString("QuitTime", DateTime.Now.ToBinary().ToString());
        OnClosed?.Invoke();
    }
    
    private void OnApplicationQuit() => OnClose();
}