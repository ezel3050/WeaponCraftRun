using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;


public class TestScript : MonoBehaviour
{
    public AssetReference canvasRefrence;
    public string path;
    private void Start()
    {
        this.CallWithDelay(() =>
        {
            canvasRefrence.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) =>
            {
                this.CallWithDelay(() =>
                {
                    Instantiate(asyncOperationHandle.Result);

                    this.CallWithDelay(() =>
                    {
                        SceneManager.LoadSceneAsync(2);
                    }, 1);

                }, 0.2f);
            };
        }, 0.5f);
    }

    private IEnumerator LoadThing()
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(path);
        yield return resourceRequest;
        GameObject thing = resourceRequest.asset as GameObject;
        Instantiate(thing);
    }
}
