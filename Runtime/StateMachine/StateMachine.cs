using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Botaemic.Core
{
    public class StateMachine<T>
    {
        //TODO override return value is something special happens

        private Dictionary<Type, State<T>> availableStates;
        public event Action<State<T>> OnStateChanged;

        public State<T> CurrentState { get; set; }
        public T owner;

        public StateMachine(T owner, Dictionary<Type, State<T>> states)
        {
            this.owner = owner;
            availableStates = states;
            CurrentState = availableStates.Values.First();
        }

        public void Update()
        {
            if (CurrentState == null)
            {
                CurrentState = availableStates.Values.First();
            }
            var nextState = CurrentState?.UpdateState();


            if (nextState != null && nextState != CurrentState?.GetType())
            {
                if (availableStates.ContainsKey(nextState))
                {
                    SwitchToNewState(nextState);
                }
                else
                {
                    SwitchToNewState(availableStates.Keys.First());
#if UNITY_EDITOR
                    Debug.LogError("Trying to switch to an unknown state: " + nextState);
                    Debug.Break();
#endif
                }
            }
        }

        private void SwitchToNewState(Type nextState)
        {
            CurrentState.ExitState();
            CurrentState = availableStates[nextState];
            CurrentState.EnterState();

            OnStateChanged?.Invoke(CurrentState);
        }
    }
}