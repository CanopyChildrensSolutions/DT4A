using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sentiment
{
    public class AnalysisViewModel : MonoBehaviour
    {
        public event Action<EmotionAnalysis> Reaction;
        public EmotionAnalysis state;

        public void SetState(EmotionAnalysis state)
        {
            this.state = state;
            Reaction.Invoke(this.state);
        }
    }
}