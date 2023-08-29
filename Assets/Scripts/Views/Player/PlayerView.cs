using UnityEngine;
using WorkShop.Commands.Player;
using WorkShop.LightWeightFramework;
using WorkShop.Models;
using WorkShop.Models.TransformModels;
using WorkShop.Services.Player;

namespace WorkShop.Views
{
    public class PlayerView : View<IPlayerModelObserver, IPlayerCommand>
    {
        [SerializeField] private Renderer meshRenderer;
        private ITransformModelObserver transformModelObserver;

        protected override void OnInit(IPlayerModelObserver model)
        {
            Model.OnControlStateChanged += OnControlStateChanged;
        }

        private void OnControlStateChanged(PlayerControlState controlState)
        {
            meshRenderer.enabled = controlState == PlayerControlState.ThirdPerson;//temp
        }

        protected override void OnRelease()
        {
            Model.OnControlStateChanged -= OnControlStateChanged;
        }

        protected override void OnCommandSet(IPlayerCommand command)
        {
            
        }
    }
}