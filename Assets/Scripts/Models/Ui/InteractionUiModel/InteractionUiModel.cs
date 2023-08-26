using System;
using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.Ui.InteractionUiModel
{
    public interface IInteractionModelObserver:IModelObserver
    {
        event Action<bool> OnInteraction;
    }
    [CreateAssetMenu(fileName = "InteractionUiModel", menuName = "Models/InteractionUiModel")]
    public class InteractionUiModel : Model, IInteractionModelObserver
    {
        public event Action<bool> OnInteraction;

        public void UpdateState(bool hasInteraction)
        {
            OnInteraction?.Invoke(hasInteraction);
        }
    }
}