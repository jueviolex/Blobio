﻿using Events;
using UnityEngine;
using Utils;
using WorldObjects;

namespace Slimes
{
    public abstract class Slime : EventListenerMono, IPausable
    {
        public int Score => _score;
        [SerializeField] protected int _score;

        [SerializeField] private Transform _rigTrans; // Declare a field for the model's Transform.

        protected virtual void IncreaseSize(int size)
        {
            _rigTrans.localScale = SlimeF.CalcRigTransLocalScale(size);  // Increase the size of the modelTransform.
        }

        protected virtual void OnBaitCollision(Bait colBait)
        {
            _score ++;
            IncreaseSize(_score); // You can change this value to your liking
        }

        protected abstract void Pause();
        protected abstract void UnPause();

        void IPausable.UnPause()
        {
            UnPause();
        }

        void IPausable.Pause()
        {
            Pause();
        }

        protected override void RegisterEvents()
        {
            GameStateEvents.Pause += OnPause;
        }
        
        private void OnPause(bool isPaused)
        {
            if(isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
        
        protected override void UnRegisterEvents()
        {
            GameStateEvents.Pause -= OnPause;
        }
    }

    public interface IPausable
    {
        void Pause();

        void UnPause();
    }
}