using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Sentiment
{
    public class SentimentRequest
    {
        public SentimentState Result;
        const string uriBase = "https://westus.api.cognitive.microsoft.com/face/v1.0/detect";

        [Serializable]
        private class EmotionDTO
        {
            public float anger;
            public float contempt;
            public float disgust;
            public float fear;
            public float happiness;
            public float neutral;
            public float sadness;
            public float surprise;

            public SentimentState ConvertToEmotionalState()
            {
                float[] confidence = new float[(int)Emotion.Count];
                confidence[(int)Emotion.Anger] = this.anger;
                confidence[(int)Emotion.Contempt] = this.contempt;
                confidence[(int)Emotion.Disgust] = this.disgust;
                confidence[(int)Emotion.Fear] = this.fear;
                confidence[(int)Emotion.Happiness] = this.happiness;
                confidence[(int)Emotion.Neutral] = this.neutral;
                confidence[(int)Emotion.Sadness] = this.sadness;
                confidence[(int)Emotion.Surprise] = this.surprise;
                return new SentimentState(confidence);
            }
        }
        [Serializable]
        private class FaceAttributesDTO
        {
            public EmotionDTO emotion;
        }

        [Serializable]
        private class FaceInfoDTO
        {
            public string faceId;
            public FaceAttributesDTO faceAttributes;
        }

        [Serializable]
        private class FaceInfoSetDTO
        {
            public FaceInfoDTO[] faceInfos;
        }

        public IEnumerator Send(byte[] imageBytes)
        {
            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=emotion";

            string uri = uriBase + "?" + requestParameters;

            UnityWebRequest www = new UnityWebRequest(uri)
            {
                method = UnityWebRequest.kHttpVerbPOST,
                uploadHandler = new UploadHandlerRaw(imageBytes)
                {
                    contentType = "application/octet-stream"
                },
                downloadHandler = new DownloadHandlerBuffer()
            };

            www.SetRequestHeader("Ocp-Apim-Subscription-Key", "ea9fdae1cb6b4217b6a18db65ce710b3");
            www.chunkedTransfer = false;

            yield return www.SendWebRequest();

            if (www.responseCode != 200)
            {
                Debug.LogError($"Upload failed: http response code = {www.responseCode}, error = {www.error}");
            }
            else
            {
                Debug.Log("Upload complete!");
                Debug.Log(www.downloadHandler.text);
                Result = ParseJson(www.downloadHandler.text);
            }
        }
        private SentimentState ParseJson(string json)
        {
            json = string.Format("{{ \"{0}\" : {1}}}", "faceInfos", json);
            var faces = JsonUtility.FromJson<FaceInfoSetDTO>(json);
            if (faces.faceInfos != null && faces.faceInfos.Length > 0)
            {
                return faces.faceInfos[0].faceAttributes.emotion.ConvertToEmotionalState();
            }
            else return default(SentimentState);
        }
    }

}