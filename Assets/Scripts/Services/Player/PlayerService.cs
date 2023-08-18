using System;
using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Player
{
    public interface IPlayerService:IService
    {
        Vector3 PlayerPosition { get; }
        void UpdatePosition(Vector3 position);
    }
    
    public class PlayerService:Service, IPlayerService
    {
        public Vector3 PlayerPosition { get; private set; }

        protected override void OnInit(IGameObserver gameObserver)
        {
            
        }

        protected override void Release()
        {
            
        }

        public void UpdatePosition(Vector3 position)
        {
            PlayerPosition = position;
        }
    }
}