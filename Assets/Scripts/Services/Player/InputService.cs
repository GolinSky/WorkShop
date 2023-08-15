using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Player
{
    public interface IInputService:IService
    {
        Vector2 UserInput { get; }

    }
    //todo:use new input system(2020 input sys)
    public class InputService: Service, IInputService
    {
        private Vector2 userInput;
        
        public Vector2 UserInput
        {
            get
            {
                userInput.x = Input.GetAxis("Horizontal");
                userInput.y = Input.GetAxis("Vertical");
                return userInput;
            }
        }
        
        protected override void OnInit(IGameObserver gameObserver)
        {
            
        }

        protected override void Release()
        {
            
        }

    }
}