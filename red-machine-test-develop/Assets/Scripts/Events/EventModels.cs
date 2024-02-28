using UnityEngine;

namespace Events
{
    public static class EventModels
    {
        public static class Game
        {
            public struct NodeTapped : IEvent
            {
                
            }
            
            public struct ScrollingTapped : IEvent
            {
                
            }

            public struct PlayerFingerRemoved : IEvent
            {
                
            }

            public struct TargetColorNodesFilled : IEvent
            {
                
            }
        }
    }
}