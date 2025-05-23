﻿using Code.Scripts.Common.StateMachines;
using Code.Scripts.Data;

namespace Code.Scripts.GameStates
{
    public class GameOverState : IState
    {
        private readonly ViewsState _viewsState;

        public GameOverState(ViewsState viewsState)
        {
            _viewsState = viewsState;
        }

        public void Enter()
        {
            _viewsState.IsGameOverVisible.Value = true;
        }

        public void Exit()
        {
            _viewsState.IsGameOverVisible.Value = false;
        }
    }
}