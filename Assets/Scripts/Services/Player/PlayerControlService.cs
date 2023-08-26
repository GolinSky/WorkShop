using System;
using WorkShop.LightWeightFramework.Game;
using WorkShop.LightWeightFramework.Service;

namespace WorkShop.Services.Player
{
    public enum PlayerControlState
    {
        ThirdPerson = 0,
        AirCraft = 1,
    }

    public interface IPlayerControlService: IService
    {
        event Action<PlayerControlState> OnControlStateChanged;
        PlayerControlState CurrentState { get; }
        void SwitchState(PlayerControlState state);
    }
    
    public class PlayerControlService:Service, IPlayerControlService
    {
        private const PlayerControlState DefaultState = PlayerControlState.ThirdPerson;
        
        public event Action<PlayerControlState> OnControlStateChanged;
        public PlayerControlState CurrentState { get; private set; }

        protected override void OnInit(IGameObserver gameObserver)
        {
            SwitchState(DefaultState);
        }
        protected override void Release() {}

        public void SwitchState(PlayerControlState state)
        {
            CurrentState = state;
            OnControlStateChanged?.Invoke(CurrentState);
        }
    }
}