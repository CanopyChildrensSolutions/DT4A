using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentiment
{
    [RequireComponent(typeof(Renderer))]
    public class VideoViewTexture : MonoBehaviour
    {
        void Start()
        {
            var videoFeed = GetComponentInParent<VideoFeed>();
            var renderer = GetComponent<Renderer>();

            var mpb = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(mpb);

            mpb.SetTexture("_MainTex", videoFeed.LiveTexture);

            renderer.SetPropertyBlock(mpb);
        }
    }
}
