using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

namespace DamphelDev.UniRX.Examples
{
    public class ReactiveTimer : MonoBehaviour
    {
        [SerializeField] TMP_Text timerText;
        [SerializeField] int countDownValue = 5;

        private IDisposable timerDisposable;

        private void Start()
        {
            timerDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeWhile(time => time <= 5)
                .Subscribe(time => DisplayTimer((int)(countDownValue-time)));
        }

        private void DisplayTimer(int remainingTime)
        {
            timerText.text = remainingTime.ToString();

            if (remainingTime <= 0)
                StopTimer();
        }

        private void StopTimer()
        {
            timerText.text = "Go!";
            timerDisposable.Dispose();
        }
    }
}
