using System;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;
using WorkShop.Models.Input;

namespace WorkShop.Services.Player
{
    public interface IInputService:IService
    {
  
    }
    //todo:use new input system(2020 input sys)
    public class InputService: Service, IInputService
    {

        

        protected override void OnInit(IGameObserver gameObserver)
        {
            
        }

        protected override void Release()
        {
            
        }

   
    }
}