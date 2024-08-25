using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DamphelDev.UniTaskExamples
{
    public class LoadImageUniTask : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private string _url = "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEiGRJ_wRXon3Dz1YTFGj_aYZp8Z_B34wVK9TPaRZyqZfPP4XK0hRgqAayI8DR0YPl1w-ms4fPI95aJI5WwPSyV9YTZkueSN8PLbPCGkz5YRR1CvHHvjbYE30FU8K-qpECqj9jMT0zyeg_w/s1600/unity.png";

        async void Start()
        {
            _image.sprite = await GetImageFromWebRequest(_url, this.GetCancellationTokenOnDestroy());
            _image.preserveAspect = true;
        }

        private async UniTask<Sprite> GetImageFromWebRequest(string url, CancellationToken token)
        {
            var unityWebRequestTexture = await UnityWebRequestTexture
                .GetTexture(url)
                .SendWebRequest()
                .WithCancellation(token);

            Texture2D texture = ((DownloadHandlerTexture)unityWebRequestTexture.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            return sprite;
        }
    }
}
