using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public Action onDone;
    public AsyncOperation asyncLoad;

    [HideInInspector] public List<GameObject> dontDestroyOnLoadGameObjects;

    private void Awake() => instance = this;

    public void LoadScene(int index, bool allowSceneActivation = false, LoadSceneMode loadSceneMode = LoadSceneMode.Single, Action<AsyncOperation> onComplete = null)
    {
        asyncLoad = SceneManager.LoadSceneAsync(index, loadSceneMode);
        asyncLoad.completed += operation => onComplete?.Invoke(operation);
        asyncLoad.allowSceneActivation = allowSceneActivation;
    }
    
    public IEnumerator LoadScene(int index, float delay, bool allowSceneActivation = false, LoadSceneMode loadSceneMode = LoadSceneMode.Single, Action<AsyncOperation> onComplete = null)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(index, allowSceneActivation, loadSceneMode, onComplete);
    }

    public void AllowSceneActivation() => asyncLoad.allowSceneActivation = true;

    public void AllowCompletion()
    {
        onDone?.Invoke();
        Destroy(gameObject);
    }
}
