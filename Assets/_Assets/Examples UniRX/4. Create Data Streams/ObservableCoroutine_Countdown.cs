using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class ObservableCoroutine_Countdown : MonoBehaviour
    {
        private void Start()
        {
            // Takes a delegate that exposes observer that we can use not only to return values
            // but also the exceptions can be handle
            Observable.FromCoroutine<int>(observer => Countdown(3, observer))
                .Subscribe(
                    i => Debug.Log(i),
                    e => Debug.LogError($"Countdown Error: {e.Message}"),
                    () => Debug.Log("Start!")
                );
        }

        private IEnumerator Countdown(int duration, IObserver<int> observer)
        {
            if (duration < 0)
                observer.OnError(new ArgumentOutOfRangeException(nameof(duration)));

            var count = duration;

            while (count >= 0)
            {
                observer.OnNext(count);
                count--;
                yield return new WaitForSeconds(1);
            }

            observer.OnCompleted();
        }
    }
}
