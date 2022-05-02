using System;
using UnityEngine;

namespace Components
{
    public class Timer
    {
        public readonly float CountdownTime;
        
        private float _timeLeft;

        public float TimeLeft => _timeLeft;

        public Timer(float countdownTime)
        {
            CountdownTime = countdownTime;
            _timeLeft = 0;
        }

        public bool IsActive => _timeLeft > 0;

        public event Action Ended;

        public void Start()
        {
            _timeLeft = CountdownTime;
        }

        public void UpdateLoop()
        {
            Update();

            if (!IsActive)
            {
                Start();
            }
        }

        public void Update()
        {
            if (_timeLeft <= 0)
            {
                return;
            }
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0)
            {
                ResetTime();
            }
        }

        public void ResetTime()
        {
            _timeLeft = 0;

            Ended?.Invoke();
        }

        public string GetTimerFormatString()
        {
            float minutes = Mathf.FloorToInt(_timeLeft / 60);
            float seconds = Mathf.FloorToInt(_timeLeft % 60);

            return $"{minutes:00} : {seconds:00}";
        }
    }
}