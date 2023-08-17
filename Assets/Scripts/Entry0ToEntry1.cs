using UnityEngine;
using UnityEngine.SceneManagement;

public class Entry0ToEntry1 : MonoBehaviour
{
    private void Start()
    {
        this.CallWithDelay(() =>
        {
            SceneManager.LoadSceneAsync(1);
        }, 1.2f);
    }
}
