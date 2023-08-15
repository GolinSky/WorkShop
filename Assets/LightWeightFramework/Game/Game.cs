using System.Collections.Generic;
using LightWeightFramework.Controller;
using LightWeightFramework.Model;
using UnityEngine.SceneManagement;
using WorkShop.LightWeightFramework.Command;
using WorkShop.LightWeightFramework.Factory;
using WorkShop.LightWeightFramework.Service;
using WorkShop.LightWeightFramework.Views;

namespace WorkShop.LightWeightFramework.Game
{
    public interface IGameObserver
    {
        IFeatureFactory Factory { get; }
        IServiceHub ServiceHub { get; }
        IModelHub ModelHub { get; }
    }

    public interface IGame : IGameObserver
    {
        void LoadLevel(IGameLevelData levelData);
        void Release();
    }

    public class Game : IGame
    {
        private readonly IFeatureFactory factory;
        private readonly IGameContext gameContext;
        private readonly IServiceHub serviceHub;
        private readonly IModelHub modelHub;
        private IEnumerable<IController> controllers;
        private List<IView> views = new List<IView>();

        IFeatureFactory IGameObserver.Factory => factory;
        IServiceHub IGameObserver.ServiceHub => serviceHub;
        IModelHub IGameObserver.ModelHub => modelHub;

        public Game(IFeatureFactory factory, IGameContext gameContext)
        {
            modelHub = new ModelHub();
            this.factory = factory;
            this.gameContext = gameContext;
            serviceHub = new ServiceHub(gameContext.Services);
        }

        void IGame.LoadLevel(IGameLevelData levelData)
        {
            if (SceneManager.GetActiveScene().buildIndex != levelData.SceneIndex) 
            {
                SceneManager.LoadScene(levelData.SceneIndex); //move to lvl service
            } 
            controllers = levelData.GetEntities(this);
            foreach (var entity in controllers)
            {
                entity.Init(this);
                var view = factory.CreateView(entity.Id);
                if (view != null)
                {
                    view.Init(entity.Model);
                    views.Add(view);
                    if (view is ICommandInvoker commandInvoker)
                    {
                        commandInvoker.SetCommand(new CommandFactory().CreateCommand(entity));
                    }
                }
            }
        }

        void IGame.Release()
        {
            foreach (var entity in controllers)
            {
                entity.Release();
            }

            foreach (var view in views)
            {
                view.Release();
            }
        }
    }
}