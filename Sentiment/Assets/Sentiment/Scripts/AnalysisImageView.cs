using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentiment
{
    public class AnalysisImageView : MonoBehaviour
    {
        AnalysisViewModel ViewModel;
        // Use this for initialization
        void Start()
        {
            ViewModel = GetComponentInParent<AnalysisViewModel>();
            ViewModel.Reaction += Render;
        }
        public void Render(EmotionAnalysis state)
        {
            var renderer = GetComponent<Renderer>();

            var mpb = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(mpb);
            mpb.SetTexture("_MainTex", state.ResultImage);

            renderer.SetPropertyBlock(mpb);
        }
    }
}