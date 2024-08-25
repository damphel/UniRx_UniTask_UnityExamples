using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DamphelDev.UniTaskExamples
{ 
    public class LoadIImageWhenAll : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private string[] _urls;

        async void Start()
        { 
            List<UniTask<Sprite>> getSpriteTasks = new List<UniTask<Sprite>>();
            
            foreach (var url in _urls)
            {
                getSpriteTasks.Add(GetImageFromWebRequest(url, this.GetCancellationTokenOnDestroy()));
            }

            Sprite[] sprites = await UniTask.WhenAll(getSpriteTasks);
            for(int i = 0; i < sprites.Length; i++)
            {
                _images[i].sprite = sprites[i];
            }
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
