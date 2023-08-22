using Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public AssetReference canvasRefrence;
    private void Start()
    {
        this.CallWithDelay(() =>
        {
            if (UIManager.Instance == null)
            {


                canvasRefrence.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) =>
                {
                    this.CallWithDelay(() =>
                    {
                        Instantiate(asyncOperationHandle.Result);
                    }, 1);
                };
            }

            else
            {
                SceneManager.LoadSceneAsync(2);
            }
        }, 1);
    }


}
