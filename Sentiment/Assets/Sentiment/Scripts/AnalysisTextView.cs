using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sentiment
{
    public class AnalysisTextView : MonoBehaviour
    {
        AnalysisViewModel ViewModel;
        TextMesh Text;
        // Use this for initialization
        void Start()
        {
            ViewModel = GetComponentInParent<AnalysisViewModel>();
            ViewModel.Reaction += Render;
            Text = GetComponent<TextMesh>();
        }
        public void Render(EmotionAnalysis state)
        {
            Text.text = "Glad: " + state.Glad + "\n";
            Text.text += "Sad: " + state.Sad + "\n";
            Text.text += "Mad: " + state.Mad;
        }
    }
}