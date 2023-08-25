using UnityEngine;
using UnityEngine.AddressableAssets;
using WorkShop.LightWeightFramework.Repository;

namespace WorkShop.Repository
{
    public class AddressableRepository:IRepository
    {
        public TSource Load<TSource>(string key) where TSource : Object
        {
#if UNITY_EDITOR
            Debug.Log($"Load {typeof(TSource)} with key:{key}");
#endif
            return Addressables.LoadAssetAsync<TSource>(key).WaitForCompletion();
        }
    }
}