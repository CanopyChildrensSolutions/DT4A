using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sentiment
{
    [Serializable]
    public struct EmotionAnalysis
    {
        public float Glad;
        public float Sad;
        public float Mad;
        [Range(0, 1)]
        public float GladThresHold;
        [Range(0, 1)]
        public float SadThresHold;
        [Range(0, 1)]
        public float MadThresHold;
        public Texture2D ResultImage;
    }
}

