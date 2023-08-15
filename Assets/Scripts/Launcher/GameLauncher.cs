using System;
using System.Collections.Generic;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using UnityEngine;
using WorkShop.Controllers;
using WorkShop.LightWeightFramework.Factory;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Repository;
using WorkShop.LightWeightFramework.Service;
using WorkShop.Models;

namespace WorkShop.Launcher
{
    public class MyGameContext : IGameContext
    {
        public IService[] Services { get; }
    }

    //test not system temp solution
    public class MyLevelData : IGameLevelData
    {
        public int SceneIndex => 0;

        public IEnumerable<IController> GetEntities(IGameObserver gameObserver)
        {
            return new List<IController>()
            {
                { CreateEntity<PlayerController, PlayerModel>(gameObserver) }
            };
        }

        private IController CreateEntity<TController, TModel>(IGameObserver gameObserver)
            where TController : Controller<TModel>
            where TModel : IModel, new()
        {
            var model = gameObserver.ModelHub.GetModel<TModel>();
            var controller = gameObserver.Factory.CreateController<TController, TModel>(model);
            return controller;
        }
    }

    public class GameLauncher : MonoBehaviour
    {
        private IGame Game;
        private MyLevelData levelData = new MyLevelData();

        private void Awake()
        {
            Game = new Game(new FeatureAbstractFactory(new ResourceRepository()), new MyGameContext());
            Game.LoadLevel(levelData);
        }

        private void OnDestroy()
        {
            Game.Release();
        }
    }
}