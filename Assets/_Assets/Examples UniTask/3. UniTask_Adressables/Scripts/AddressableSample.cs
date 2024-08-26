using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

namespace DamphelDev.UniTaskExamples
{
    public class AddressableSample : MonoBehaviour
    {
        [SerializeField] private AssetReference _cube;
        async void Start()
        {
            GameObject result = await _cube.GetGameObject();
            Instantiate(result);
        }
    }

}
public static class AdressableUniTaskExt
{
    public static async UniTask<GameObject> GetGameObject(this AssetReference assetReference)
    { 
        UniTask<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(assetReference).Task.AsUniTask();
        GameObject result = await asyncOperationHandle;
        return result;
    }
}
