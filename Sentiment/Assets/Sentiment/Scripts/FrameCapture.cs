using System.IO;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace Sentiment
{
    public class FrameCapture : MonoBehaviour
    {
        GestureRecognizer gestureRecognizer;

        void Awake()
        {
            gestureRecognizer = new GestureRecognizer();
            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
            gestureRecognizer.Tapped += GestureRecognizer_Tapped;
        }

        // Use this for initialization
        void Start()
        {
            gestureRecognizer.StartCapturingGestures();
        }

        private void GestureRecognizer_Tapped(TappedEventArgs obj)
        {
            SaveFrame();
        }
        
        void Update()
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.P))
            {
                SaveFrame();
            }
        }

        void SaveFrame()
        {
            var videoFeed = GetComponentInParent<VideoFeed>();
            byte[] pngBytes = videoFeed.EncodeCurrentFrameAsPNG();

            
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Test.png"), pngBytes);
        }
    }
}
