using System.Collections.Generic;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Controllers;
using WorkShop.Controllers.Camera;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Models;
using WorkShop.Models.Camera;

namespace WorkShop.Launcher
{
    public class MyLevelData : IGameLevelData
    {
        public int SceneIndex => 0;

        public IEnumerable<IController> GetEntities(IGameObserver gameObserver)
        {
            return new List<IController>()
            {
                { CreateEntity<PlayerController, PlayerModel>(gameObserver) },
                { CreateEntity<CameraController, CameraModel>(gameObserver) }
            };
        }

        private IController CreateEntity<TController, TModel>(IGameObserver gameObserver)
            where TController : Controller<TModel>
            where TModel : Object,IModel
        {
            var model = gameObserver.ModelHub.GetModel<TModel>(typeof(TModel).Name); // todo: fix id 
            var controller = gameObserver.Factory.CreateController<TController, TModel>(model);
            return controller;
        }
        

    }
}