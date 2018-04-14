using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisImageView : MonoBehaviour
{
    AnalysisViewModel ViewModel;
    // Use this for initialization
    void Start()
    {
        ViewModel = FindObjectOfType<AnalysisViewModel>();
        ViewModel.Reaction += Render;
    }
    public void Render(AnalysisViewModel.Analysis analysis)
    {
        var renderer = GetComponent<Renderer>();

        var mpb = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(mpb);
        mpb.SetTexture("_MainTex", analysis.ResultImage);

        renderer.SetPropertyBlock(mpb);
    }
}
