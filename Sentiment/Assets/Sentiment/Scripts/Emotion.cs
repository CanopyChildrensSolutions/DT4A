namespace Sentiment
{
    public enum Emotion : int
    {
        First = 0,

        Anger = First,
        Contempt,
        Disgust,
        Fear,
        Happiness,
        Neutral,
        Sadness,
        Surprise,

        Last = Surprise,
        Count
    }
}