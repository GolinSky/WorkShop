using System.Collections.Generic;
using GofPatterns.Patterns.Behavioral.Observer.Custom;
using WorkShop.LightWeightFramework.Game;

namespace WorkShop.LightWeightFramework.Service
{
    public interface ITickService:IService, ICustomSubject<float>
    {
       
    }
    public class TickService:Service, ITickService
    {
        private List<ICustomObserver<float>> observers = new List<ICustomObserver<float>>();
        protected override void OnInit(IGameObserver gameObserver)
        {
            
        }

        protected override void Release()
        {
            observers.Clear();
        }

        public void Update(float deltaTime)
        {
            for (var i = 0; i < observers.Count; i++)
            {
                observers[i].Notify(deltaTime);
            }
        }

        void ICustomSubject<float>.AddObserver(ICustomObserver<float> observer)
        {
            observers.Add(observer);
        }

        void ICustomSubject<float>.RemoveObserver(ICustomObserver<float> observer)
        {
            observers.Remove(observer);
        }
    }
}