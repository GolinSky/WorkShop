using UnityEngine;
using UnityEngine.AddressableAssets;
using WorkShop.LightWeightFramework.Repository;

namespace WorkShop.Repository
{
    public class AddressableRepository:IRepository
    {
        public TSource Load<TSource>(string key) where TSource : Object
        {
            var obj = Addressables.LoadAssetAsync<TSource>(key).WaitForCompletion();
            return obj;
        }
    }
}