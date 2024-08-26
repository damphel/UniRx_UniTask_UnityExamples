using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DamphelDev.UniTaskExamples
{
    public class CoroutineWebRequest : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SendRequest("https://reqres.in/api/users?page=2"));
        }

        IEnumerator SendRequest(string url)
        { 
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            { 
                Debug.LogError(string.Format("Error: {0}", request.error));
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }

        }
    }
}
