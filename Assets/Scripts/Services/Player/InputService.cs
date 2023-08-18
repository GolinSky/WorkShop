using UnityEngine;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Player
{
    public interface IInputService:IService
    {
        Vector2 UserInput { get; }
        Vector2 MouseAxisInput { get; }

    }
    //todo:use new input system(2020 input sys)
    public class InputService: Service, IInputService
    {
        private Vector2 userInput;
        private Vector2 mouseAxisInput;

        public Vector2 UserInput
        {
            get
            {
                userInput.x = Input.GetAxis("Horizontal");
                userInput.y = Input.GetAxis("Vertical");
                return userInput;
            }
        }

        public Vector2 MouseAxisInput
        {
            get
            {
                mouseAxisInput.x = Input.GetAxis("Mouse X");
                mouseAxisInput.y = Input.GetAxis("Mouse Y");
                return mouseAxisInput;
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