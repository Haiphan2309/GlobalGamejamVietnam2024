using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shun_State_Machine
{
    /// <summary>
    /// This is a StateHistory using a stack, save and restore form the top of the stack
    /// </summary>
    /// <typeparam name="TStateEnum"></typeparam>
    public class StackStateHistoryStrategy<TStateEnum> : IStateHistoryStrategy<TStateEnum> where TStateEnum : Enum
    {
        private int _maxSize = 0;
        private LinkedList<(BaseState<TStateEnum>, IStateParameter, IStateParameter)> _historyStates = new(); // act as a stack

        public StackStateHistoryStrategy(int maxSize = 100)
        {
            _maxSize = maxSize;
        }
        
        public void Save(BaseState<TStateEnum> baseState, IStateParameter exitOldStateParameters = null, IStateParameter enterNewStateParameters = null)
        {
            if (_historyStates.Count >= _maxSize)
            {
                _historyStates.RemoveLast();
            }

            _historyStates.AddFirst((baseState, exitOldStateParameters, enterNewStateParameters));
        }

        public void Save(TStateEnum stateEnum, IStateParameter exitOldStateParameters = null, IStateParameter enterNewStateParameters = null)
        {
            throw new NotImplementedException();
        }

        public (BaseState<TStateEnum> enterStateEnum, IStateParameter exitOldStateParameters, IStateParameter enterNewStateParameters) Restore(bool isRemoveRestore = true)
        {
            if (_historyStates.Count != 0)
            {
                var (lastState, exitOldStateParameters, enterNewStateParameters) = _historyStates.First.Value;
                 
                if(isRemoveRestore) _historyStates.RemoveFirst();
                
                return (lastState, exitOldStateParameters, enterNewStateParameters);
            }
            else
            {
                Debug.LogError("No state in the history state machine");
                return (default, default, default);
            }
        }
    }
}