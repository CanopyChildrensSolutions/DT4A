using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentiment
{
    public class VideoFeed : MonoBehaviour
    {
        WebCamTexture webCam;

        public bool AutoStart = true;

        public Texture LiveTexture {  get { return webCam; } }

        public byte[] EncodeCurrentFrameAsPNG()
        {
            var texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
            texture.SetPixels32(webCam.GetPixels32());
            return texture.EncodeToPNG();
        }

        private void Awake()
        {
            webCam = new WebCamTexture();
        }

        // Use this for initialization
        void Start()
        {
            if( AutoStart)
            {
                Play();
            }
        }

        private void OnDestroy()
        {
            if (webCam != null)
            {
                if (webCam.isPlaying)
                {
                    webCam.Stop();
                }

                webCam = null;
            }
        }

        public void Play()
        {
            webCam.Play();
        }

        public void Stop()
        {
            webCam.Stop();
        }
    }
}
