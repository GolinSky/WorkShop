using LightWeightFramework.Model;
using UnityEngine;

namespace WorkShop.Models.Input
{
    public interface IInputModelObserver : IModelObserver
    {
        Vector2 Move { get; }
        Vector2 Look { get; }
        bool Jump { get; set; }//fix
        bool Sprint { get; }

        bool AnalogMovement { get; }

        bool CursorLocked { get; }
        bool CursorInputForLook { get; }
    }

    [CreateAssetMenu(fileName = "InputModel", menuName = "Models/InputModel", order = 1)]
    public class InputModel : Model, IInputModelObserver
    {
        public Vector2 Move { get; set; }
        public Vector2 Look { get; set; }
        public bool Jump { get; set; }
        public bool Sprint { get; set; }

        [field: SerializeField] public bool AnalogMovement { get; private set; }
        [field: SerializeField] public bool CursorLocked { get; private set; }
        [field: SerializeField] public bool CursorInputForLook { get; private set; }
    }
}