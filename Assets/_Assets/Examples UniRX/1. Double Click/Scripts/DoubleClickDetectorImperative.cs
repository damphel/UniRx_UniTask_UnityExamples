using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamphelDev.UniRX.Examples.DoubleClick
{
    public class DoubleClickDetectorImperative : MonoBehaviour
    {
        private float _lastClickTime;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            if (Time.time - _lastClickTime < 0.25f)
                Debug.Log("Double Click Imperative");

            _lastClickTime = Time.time;         
        }
    }
}
