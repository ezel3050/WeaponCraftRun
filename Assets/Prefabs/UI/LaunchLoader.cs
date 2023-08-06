using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchLoader : MonoBehaviour
{
    public OnCloseListener onCloseListener;

    private void Start()
    {
        this.CallWithDelay(() =>
        {
            LoadGame();
        }, 1.25f);
    }

    private void LoadGame() => SceneLoader.instance.LoadScene(2, true);
    
}