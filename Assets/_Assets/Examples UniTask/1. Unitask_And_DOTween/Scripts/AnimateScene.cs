using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

namespace DamphelDev.UniTaskExamples
{
    public class AnimateScene : MonoBehaviour
    {
        [Header("Environment")]
        [SerializeField] private Transform[] outerObjectsTransform;
        [SerializeField] private Transform[] innerObjectsTransform;

        [Header("Ship")]
        [SerializeField] private Transform shipTransform;
        [SerializeField] private Transform startShipPosition;
        private Vector3 currentShipPosition;

        [Space(15)]
        [SerializeField] private TMP_Text _text;

        async void Start()
        {
            CancellationToken cancellationTokenOnDestroy = this.GetCancellationTokenOnDestroy();

            SetText("Initializing Outer Objects...");
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
            await AnimateOuterObject(outerObjectsTransform, cancellationTokenOnDestroy);
            SetText("Initializing Inner Objects...");
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
            await AnimateInnerObject(innerObjectsTransform, cancellationTokenOnDestroy);
            SetText("Moving Ship...");
            await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
            await AnimateShip(shipTransform, cancellationTokenOnDestroy);
        }

        private void SetText(string value)
        { 
            _text.text = "Status: " + value;
        }

        private async UniTask AnimateOuterObject(Transform[] objectsToAnimate, CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: cancellationToken);

            UniTask[] tasks = new UniTask[objectsToAnimate.Length];

            for (int i = 0; i < objectsToAnimate.Length; i++)
            {
                tasks[i] = AnimateDelay(objectsToAnimate[i], cancellationToken, i*0.1f, i==objectsToAnimate.Length-1);
            }

            await UniTask.WhenAll(tasks);
        }

        private async UniTask AnimateDelay(Transform objectToAnimate, CancellationToken cancellationToken, float time, bool lastObject)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: cancellationToken);
            objectToAnimate.gameObject.SetActive(true);

            if (lastObject)
                await objectToAnimate.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.InOutBack).WithCancellation(cancellationToken);
            else
                objectToAnimate.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.InOutBack).WithCancellation(cancellationToken);
        }

        private async UniTask AnimateInnerObject(Transform[] objectsToAnimate, CancellationToken cancellationToken)
        {
            for (int i = 0; i < objectsToAnimate.Length; i++)
            {
                objectsToAnimate[i].gameObject.SetActive(true);
                objectsToAnimate[i].DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBack).WithCancellation(cancellationToken);
            }
        }

        private async UniTask AnimateShip(Transform shipTransform, CancellationToken cancellationToken)
        {
            currentShipPosition = shipTransform.position;
            shipTransform.position = startShipPosition.position;
            shipTransform.gameObject.SetActive(true);

            await shipTransform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.InOutBack).WithCancellation(cancellationToken);
            await shipTransform.DOMove(currentShipPosition, .7f).SetEase(Ease.InOutBack).WithCancellation(cancellationToken);
        }
    }
}
