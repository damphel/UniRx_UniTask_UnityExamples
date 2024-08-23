using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading;

namespace DamphelDev.UniRX.Examples.Observables
{
    public class ObservableProgram : MonoBehaviour
    {
        
    }

    class Program
    { 
        static void Main(string[] args)
        {
            // The observer that subscribed to thiis custom Observable simply writes the emitted values to the console
            CountToThree().Subscribe(i => Console.WriteLine(i));
        }
    
        static IObservable<int> CountToThree()
        {
            // Create takes a delegate method that provides access to the observer
            // This is were you can call OnNext, OnError, OnCompleted
            return Observable.Create<int>(observer =>
            {
                // The delegate loops through an array of numbers emitting each value and wating 1 second
                foreach (var i in new[] { 1, 2, 3 })
                {
                    observer.OnNext(i);
                    Thread.Sleep(1000);
                }
                observer.OnCompleted();

                return Disposable.Empty;
            });
        }
    }
}
