using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Cursor
{
    public class CursorService:Service
    {
        protected override void OnInit(IGameObserver gameObserver)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void Release()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
}