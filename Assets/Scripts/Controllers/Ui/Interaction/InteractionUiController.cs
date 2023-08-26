using LightWeightFramework.Controller;
using WorkShop.Models.Ui.InteractionUiModel;
using WorkShop.Services.Interaction;

namespace WorkShop.Controllers.Ui.Interaction
{
    public class InteractionUiController : Controller<InteractionUiModel>
    {
        private IInteractionService interactionService;

        public InteractionUiController(InteractionUiModel model) : base(model)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            interactionService = GetService<IInteractionService>();
            interactionService.OnInteractionChanged += UpdateInteractionState;
        }
        
        protected override void OnRelease()
        {
            base.OnRelease();
            interactionService.OnInteractionChanged -= UpdateInteractionState;
        }

        private void UpdateInteractionState(bool hasInteraction)
        {
            Model.UpdateState(hasInteraction);
        }
    }
}