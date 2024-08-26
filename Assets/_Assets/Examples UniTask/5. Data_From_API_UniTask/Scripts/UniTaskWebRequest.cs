using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace DamphelDev.UniTaskExamples
{
    public class UniTaskWebRequest : MonoBehaviour
    {
        async private void Start()
        {
            /*UnityWebRequest op = await UnityWebRequest
                .Get("https://reqres.in/api/users?page=2")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());

            Debug.Log(op.downloadHandler.text);*/

            string result = await SendRequest("https://reqres.in/api/users?page=2", this.GetCancellationTokenOnDestroy());
            Debug.Log(result);

            GetWebRequestTimeout();
        }

        private async UniTask<string> SendRequest(string url, CancellationToken token)
        {
            UnityWebRequest op = await UnityWebRequest
                .Get(url)
                .SendWebRequest()
                .WithCancellation(token);

            return op.downloadHandler.text;
        }

        async void GetWebRequestTimeout()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromSeconds(3)); // 3sec timeout

            try
            {
                await UnityWebRequest.Get("https://reqres.in/api/users?page=2")
                    .SendWebRequest()
                    .WithCancellation(cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    Debug.LogError("Timeout");
                }
            }

        }
    }
}
