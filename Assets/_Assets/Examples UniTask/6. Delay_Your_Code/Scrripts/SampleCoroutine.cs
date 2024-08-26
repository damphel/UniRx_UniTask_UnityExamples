using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamphelDev.UniTaskExamples
{
    public class SampleCoroutine : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(InvokeMethodDelay(() =>
            {
                Debug.Log("Hello World");
            }));
        }

        IEnumerator InvokeMethodDelay(Action callback)
        { 
            yield return new WaitForSeconds(1f);
            callback.Invoke();
        }
    }
}
