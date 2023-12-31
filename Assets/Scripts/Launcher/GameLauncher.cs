using System;
using UnityEngine;
using WorkShop.LightWeightFramework.Factory;
using WorkShop.LightWeightFramework.Game;
using WorkShop.Repository;

namespace WorkShop.Launcher
{
    //test,not system, temp solution

    public class GameLauncher : MonoBehaviour
    {
        private IGame Game;
        private MyLevelData levelData = new MyLevelData();

        private void Awake()
        {
            Game = new Game(new FeatureAbstractFactory(new AddressableRepository()), new MyGameContext());
            Game.LoadLevel(levelData);
        }

        private void OnDestroy()
        {
            Game.Release();
        }

        private void Update()
        {
            Game.Update();
        }
    }
}