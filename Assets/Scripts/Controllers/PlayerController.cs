using LightWeightFramework.Controller;
using UnityEngine;
using WorkShop.Models;

namespace WorkShop.Controllers
{
    public class PlayerController: Controller<PlayerModel>
    {
        public override string Id => "Player";

        public PlayerController(PlayerModel model) : base(model)
        {
            Model.UpdatePosition(new Vector3(0,10,0));
        }
    }
}