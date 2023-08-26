using UnityEngine;
using WorkShop.LightWeightFramework;
using WorkShop.Models.Ui.InteractionUiModel;

namespace WorkShop.Views.Ui
{
    public class InteractionUiView : View<IInteractionModelObserver>
    {
        [SerializeField] private Canvas interactionCanvas;

        protected override void OnInit(IInteractionModelObserver model)
        {
            Model.OnInteraction += OnInteraction;
        }

        protected override void OnRelease()
        {
            Model.OnInteraction -= OnInteraction;
        }

        private void OnInteraction(bool hasInteraction)
        {
            interactionCanvas.enabled = hasInteraction;
        }
    }
}