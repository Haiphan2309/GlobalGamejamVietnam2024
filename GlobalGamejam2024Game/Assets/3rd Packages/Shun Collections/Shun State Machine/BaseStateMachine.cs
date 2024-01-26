using System;
using System.Collections;
using System.Collections.Generic;
using Shun_Unity_Editor;
using UnityEngine;
using UnityUtilities;

namespace Shun_State_Machine
{
    [Serializable]
    public class BaseStateMachine<TStateEnum> where TStateEnum : Enum 
    {
        [ShowImmutable, SerializeField] protected BaseState<TStateEnum> CurrentBaseState = new (default);
        private Dictionary<TStateEnum, BaseState<TStateEnum>> _states = new ();

        [Header("History")] 
        protected IStateHistoryStrategy<TStateEnum> StateHistoryStrategy;

        public void SetHistoryStrategy(IStateHistoryStrategy<TStateEnum> historyStrategy = null)
        {
            StateHistoryStrategy = historyStrategy;
        }

        public void ExecuteState(IStateParameter parameters = null)
        {
            CurrentBaseState.ExecuteState(parameters);
        }
        public void AddState(BaseState<TStateEnum> baseState)
        {
            _states[baseState.MyStateEnum] = baseState;
        }

        public void RemoveState(TStateEnum stateEnum)
        {
            _states.Remove(stateEnum);
        }

        public void SetToState(TStateEnum stateEnum, IStateParameter exitOldStateParameters = null, IStateParameter enterNewStateParameters = null)
        {
            if (_states.TryGetValue(stateEnum, out BaseState<TStateEnum> nextState))
            {
                StateHistoryStrategy?.Save(nextState, exitOldStateParameters, enterNewStateParameters);
                SwitchState(nextState, exitOldStateParameters, enterNewStateParameters);
            }
            else
            {
                Debug.LogWarning($"State {stateEnum} not found in state machine.");
            }
        }
        
        public TStateEnum GetState()
        {
            return CurrentBaseState.MyStateEnum;
        }

        private void SwitchState(BaseState<TStateEnum> nextState , IStateParameter exitOldStateParameters = null, IStateParameter enterNewStateParameters = null)
        {
            CurrentBaseState.OnExitState(nextState.MyStateEnum,exitOldStateParameters);
            TStateEnum lastStateEnum = CurrentBaseState.MyStateEnum;
            CurrentBaseState = nextState;
            nextState.OnEnterState(lastStateEnum,enterNewStateParameters);
        }
        
        public void RestoreState()
        {
            if (StateHistoryStrategy == null) return;
            var (enterState, exitOldStateParameters,enterNewStateParameters) = StateHistoryStrategy.Restore();
            if (enterState != null)
            {
                SetToState(enterState.MyStateEnum, exitOldStateParameters, enterNewStateParameters);
            }
            else SetToState(default);
            
        }
        
    }
}
