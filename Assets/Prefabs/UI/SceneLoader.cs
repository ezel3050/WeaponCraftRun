using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public Action onDone;
    public AsyncOperation asyncLoad;
    
    [HideInInspector] public List<GameObject> dontDestroyOnLoadGameObjects;

    private void Awake() => instance = this;

    public void LoadScene(int index, bool allowSceneActivation = false, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        asyncLoad = SceneManager.LoadSceneAsync(index, loadSceneMode);
        asyncLoad.allowSceneActivation = allowSceneActivation;
    }

    public IEnumerator LoadScene(int index, float delay, bool allowSceneActivation = false, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(index, allowSceneActivation, loadSceneMode);
    }

    public void AllowSceneActivation() => asyncLoad.allowSceneActivation = true;

    private void AllowCompletion()
    {
        onDone?.Invoke();
        Destroy(gameObject);
    }
}
