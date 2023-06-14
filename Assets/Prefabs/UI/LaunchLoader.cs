using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchLoader : MonoBehaviour
{
    public OnCloseListener onCloseListener;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame() => SceneLoader.instance.LoadScene(1, true);
    
}