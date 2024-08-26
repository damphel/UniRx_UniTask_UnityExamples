using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace DamphelDev.UniTaskExamples
{
    public class UniTaskWhenAllSample : MonoBehaviour
    {
        async void Start()
        { 
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //await GetWebRequest("https://reqres.in/api/users?page=2", this.GetCancellationTokenOnDestroy());
            //await GetWebRequest("https://reqres.in/api/users?page=3", this.GetCancellationTokenOnDestroy());
            //stopwatch.Stop();

            var tasks = new List<UniTask<string>>();

            tasks.Add(GetWebRequest("https://reqres.in/api/users?page=2", this.GetCancellationTokenOnDestroy()));
            tasks.Add(GetWebRequest("https://reqres.in/api/users?page=3", this.GetCancellationTokenOnDestroy()));

            await UniTask.WhenAll(tasks);

            stopwatch.Stop();
            Debug.Log($"Processing of operation Done in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        private async UniTask<string> GetWebRequest(string url, CancellationToken cancellationToken)
        {
            var op = await UnityWebRequest
                .Get(url)
                .SendWebRequest()
                .WithCancellation(cancellationToken);
            
            string result = op.downloadHandler.text;
            Debug.Log($"result is {result}");
            return result;
        }
    }
}
