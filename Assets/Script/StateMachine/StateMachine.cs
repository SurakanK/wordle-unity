using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class StateMachine
    {
        private IState _currentState;
        
        public void Initialize(IState startingState)
        {
            _currentState = startingState;
            startingState.Initialize(this);
        }

        public void ChangeState(IState newState)
        {
            _currentState.OnEnded();

            _currentState = newState;
            newState.Initialize(this);
        }
    }
}