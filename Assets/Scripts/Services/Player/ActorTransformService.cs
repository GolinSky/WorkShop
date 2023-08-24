using System;
using System.Collections.Generic;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;
using WorkShop.MonoProviders;

namespace WorkShop.Services.Player
{
    public interface IActorTransformService:IService
    {
        event Action<ActorType, IMovementProvider> OnActorAdded;
        IMovementProvider GetActorProvider(ActorType actorType);
        void AddActor(ActorType actorType, IMovementProvider transform);
        bool HasActor(ActorType actorType, out IMovementProvider provider);

    }
    
    public class ActorTransformService:Service, IActorTransformService
    {
        public event Action<ActorType, IMovementProvider> OnActorAdded;

        private Dictionary<ActorType, IMovementProvider> actorsDictionary = new Dictionary<ActorType, IMovementProvider>();
        protected override void OnInit(IGameObserver gameObserver)
        {
            
        }

        protected override void Release()
        {
            actorsDictionary.Clear();
        }


        public IMovementProvider GetActorProvider(ActorType actorType)
        {
            if (actorsDictionary.TryGetValue(actorType, out var movementProvider))
            {
                return movementProvider;
            }

            return default;
        }

        public void AddActor(ActorType actorType, IMovementProvider movementProvider)
        {
            actorsDictionary.Add(actorType, movementProvider);
            OnActorAdded?.Invoke(actorType, movementProvider);
        }

        public bool HasActor(ActorType actorType, out IMovementProvider provider)
        {
            provider = GetActorProvider(actorType);
            return provider != null;
        }
    }

    public enum ActorType
    {
        Player = 0,
        Camera = 1,
    }
}