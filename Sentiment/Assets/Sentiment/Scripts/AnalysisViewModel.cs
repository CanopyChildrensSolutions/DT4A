using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnalysisViewModel : MonoBehaviour
{
    public event Action<Analysis> Reaction;
    public Analysis state;

    [Serializable]
    public struct Analysis
    {
        public float Glad;
        public float Sad;
        public float Mad;
        [Range(0,1)]
        public float GladThresHold;
        [Range(0, 1)]
        public float SadThresHold;
        [Range(0, 1)]
        public float MadThresHold;
        public Texture2D ResultImage;
    }
    
    public void SetState(Analysis state)
    {
        this.state = state;
        Reaction.Invoke(this.state);
    }
}
