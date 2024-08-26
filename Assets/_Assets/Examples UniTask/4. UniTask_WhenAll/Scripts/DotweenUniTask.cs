using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace DamphelDev.UniTaskExamples
{
    public class DotweenUniTask : MonoBehaviour
    {
        [SerializeField] private Transform cube;
        [SerializeField] private Button _cancelButton;

        private CancellationTokenSource cts = new CancellationTokenSource();

        private async void Start()
        {
            _cancelButton.onClick.AddListener(() =>
            {
                Cancel();
            });

            UniTask moveTask = MoveTask();
            UniTask rotateTask = cube.DORotate(new Vector3(90,90,90), 2f).OnComplete(() =>
            {
                Debug.Log("Finish Rotating");
            }).ToUniTask();

            UniTask scaleTask = cube.DOScale(new Vector3(2, 2, 2), 3f).OnComplete(() =>
            {
                Debug.Log("Finish Scaling");
            }).ToUniTask();

            UniTask[] tasks = new UniTask[] { moveTask, rotateTask, scaleTask };
            UniTask whenAll = UniTask.WhenAll(tasks);

            await whenAll;

            Debug.Log("All tasks completed");
        }

        private UniTask MoveTask()
        {
            return cube.DOMove(new Vector3(5, 0, 5), 5f).WithCancellation(cts.Token).ContinueWith(() =>
            {
                Debug.Log("Finish Moving");
            });
        }

        public void Cancel()
        {
            cts.Cancel();
        }
    }
}
