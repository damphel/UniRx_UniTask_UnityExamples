using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DamphelDev.UniTaskExamples
{
    public class LoadImageCoroutine : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private string _url = "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEiGRJ_wRXon3Dz1YTFGj_aYZp8Z_B34wVK9TPaRZyqZfPP4XK0hRgqAayI8DR0YPl1w-ms4fPI95aJI5WwPSyV9YTZkueSN8PLbPCGkz5YRR1CvHHvjbYE30FU8K-qpECqj9jMT0zyeg_w/s1600/unity.png";

        private void Start()
        {
            StartCoroutine(LoadImagenFromURL(_url));
        }

        public IEnumerator LoadImagenFromURL(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(www.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    Sprite _sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    _image.sprite = _sprite;
                    _image.preserveAspect = true;
                    break;
            }
        }
    }
}
