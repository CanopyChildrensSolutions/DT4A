using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisTextView : MonoBehaviour
{
    AnalysisViewModel ViewModel;
    TextMesh Text;
    // Use this for initialization
    void Start()
    {
        ViewModel = FindObjectOfType<AnalysisViewModel>();
        ViewModel.Reaction += Render;
        Text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Render(AnalysisViewModel.Analysis analysis)
    {
        Text.text = "Glad: " + analysis.Glad + "\n";
        Text.text += "Sad: " + analysis.Sad + "\n";
        Text.text += "Mad: " + analysis.Mad;
    }
}
