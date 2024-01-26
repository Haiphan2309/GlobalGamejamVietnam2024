using System;
using UnityEngine;

namespace Shun_State_Machine
{
    [Serializable]
    public class BaseState<TStateEnum> where TStateEnum : Enum
    {
        [Header("State Machine ")]
        public TStateEnum MyStateEnum;
        protected object[] Objects;
        
        protected Action<TStateEnum, IStateParameter> EnterEvents;
        protected Action<TStateEnum, IStateParameter> ExecuteEvents;
        protected Action<TStateEnum, IStateParameter> ExitEvents;

        public BaseState(TStateEnum myStateEnum,
            Action<TStateEnum, IStateParameter> executeEvents = null,
            Action<TStateEnum, IStateParameter> exitEvents = null,
            Action<TStateEnum, IStateParameter> enterEvents = null)
        {
            MyStateEnum = myStateEnum;
            EnterEvents = enterEvents;
            ExecuteEvents = executeEvents;
            ExitEvents = exitEvents;
        }
        
        public enum StateEvent
        {
            EnterState,
            ExitState,
            ExecuteState
        }

        public virtual void OnExitState(TStateEnum enterState = default, IStateParameter parameters = null)
        {
            ExitEvents?.Invoke(enterState, parameters);
        }
        
        public virtual void OnEnterState(TStateEnum exitState = default, IStateParameter parameters = null)
        {
            EnterEvents?.Invoke(exitState, parameters);
        }

        public virtual void ExecuteState(IStateParameter parameters = null)
        {
            ExecuteEvents?.Invoke(MyStateEnum, parameters);
        }

        public void SubscribeToState(StateEvent stateEvent, Action<TStateEnum, IStateParameter>[] actions )
        {
            foreach (var action in actions)
            {
                switch (stateEvent)
                {
                    case StateEvent.EnterState:
                        EnterEvents += action;
                        break;
                    case StateEvent.ExitState:
                        ExitEvents += action;
                        break;
                    case StateEvent.ExecuteState:
                        ExecuteEvents += action;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stateEvent), stateEvent, null);
                }
            }
        }

        private void UnsubscribeToState(StateEvent stateEvent, Action<TStateEnum, IStateParameter>[] actions )
        {
            foreach (var action in actions)
            {
                switch (stateEvent)
                {
                    case StateEvent.EnterState:
                        EnterEvents -= action;
                        break;
                    case StateEvent.ExitState:
                        ExitEvents -= action;
                        break;
                    case StateEvent.ExecuteState:
                        ExecuteEvents -= action;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stateEvent), stateEvent, null);
                }
            }
        }
        
    }
    
}