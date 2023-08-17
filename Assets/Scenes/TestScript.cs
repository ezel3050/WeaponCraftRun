using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public string path;
    private void Start()
    {
        this.CallWithDelay(() =>
        {
            StartCoroutine(LoadThing());
        }, 2);

        this.CallWithDelay(() =>
        {
            SceneManager.LoadSceneAsync(2);
        }, 4.5f);
    }

    private IEnumerator LoadThing()
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(path);
        yield return resourceRequest;
        GameObject thing = resourceRequest.asset as GameObject;
        Instantiate(thing);
    }
}
