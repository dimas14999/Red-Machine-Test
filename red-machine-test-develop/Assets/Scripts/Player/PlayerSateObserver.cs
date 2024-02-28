using System;
using Events;


namespace Player
{
    public class PlayerSateObserver
    {
        private readonly Action<PlayerState> _setStateAction;
        
        
        public PlayerSateObserver(Action<PlayerState> setStateAction)
        {
            _setStateAction = setStateAction;
        }

        public void Subscribe()
        {
            EventsController.Subscribe<EventModels.Game.NodeTapped>(this, OnNodeTapped);
            EventsController.Subscribe<EventModels.Game.ScrollingTapped>(this, OnScrollingTapped);
            EventsController.Subscribe<EventModels.Game.PlayerFingerRemoved>(this, OnPlayerFingerRemoved);
        }

        public void Unsubscribe()
        {
            EventsController.Unsubscribe<EventModels.Game.NodeTapped>(OnNodeTapped);
            EventsController.Unsubscribe<EventModels.Game.ScrollingTapped>(OnScrollingTapped);
            EventsController.Unsubscribe<EventModels.Game.PlayerFingerRemoved>(OnPlayerFingerRemoved);
        }

        private void OnNodeTapped(EventModels.Game.NodeTapped e)
        {
            _setStateAction?.Invoke(PlayerState.Connecting);
        }
        
        private void OnScrollingTapped(EventModels.Game.ScrollingTapped obj)
        {
            _setStateAction?.Invoke(PlayerState.Scrolling);
        }

        private void OnPlayerFingerRemoved(EventModels.Game.PlayerFingerRemoved e)
        {
            _setStateAction?.Invoke(PlayerState.None);
        }
    }
}
