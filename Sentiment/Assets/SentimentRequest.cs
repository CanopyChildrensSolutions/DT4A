using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SentimentRequest : MonoBehaviour
{

    public Texture2D TestImage;
    public Emotion Response;
    const string uriBase = "https://westus.api.cognitive.microsoft.com/face/v1.0/detect";

    [Serializable]
    public class Emotion
    {
        public float anger;
        public float contempt;
        public float disgust;
        public float fear;
        public float happiness;
        public float neutral;
        public float sadness;
        public float surprise;

        public override string ToString()
        {
            return $"anger = {anger}\n" +
                $"contempt = {contempt}\n" +
                $"disgust = {disgust}\n" +
                $"happiness = {happiness}\n" +
                $"neutral = {neutral}\n" +
                $"sadness = {sadness}\n" +
                $"surprise = {surprise}\n";
        }
    }

    [Serializable]
    public class FaceAttributes
    {
        public Emotion emotion;
    }

    [Serializable]
    public class FaceInfo
    {
        public string faceId;
        public FaceAttributes faceAttributes;
    }

    [Serializable]
    public class FaceInfoSet
    {
        public FaceInfo[] faceInfos;
    }

    public IEnumerator Upload(byte[] imageBytes)
    {
        imageBytes = TestImage.EncodeToPNG();

        // Request parameters. A third optional parameter is "details".
        string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

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

        yield return www.SendWebRequest();

        Debug.LogError($"IsNetworkError = { www.isNetworkError }, IsHttpError = {www.isHttpError}");
        if (www.responseCode != 200)
        {
            Debug.LogError(www.responseCode);
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
            Debug.Log(DownloadHandlerBuffer.GetContent(www));
            Response = ParseJson(DownloadHandlerBuffer.GetContent(www));
        }
    }
    public Emotion ParseJson(string json)
    {
        var infoTest = new FaceInfoSet() { faceInfos = new FaceInfo[1] };
        infoTest.faceInfos[0] = new FaceInfo() { faceAttributes = new FaceAttributes() { emotion = new Emotion() } };
        //infoTest.faceInfos[0].faceAttributes = new FaceAttributes() { emotion = new Emotion() { anger = 1f } };
        string test = JsonUtility.ToJson(infoTest);

        //json = $"{{ \"faceInfos\" : {json} }}"; 
        json = string.Format("{{ \"{0}\" : {1}}}", "faceInfos", json);
        var faces = JsonUtility.FromJson<FaceInfoSet>(json);
        if (faces.faceInfos != null && faces.faceInfos.Length > 0)
        {
            return faces.faceInfos[0].faceAttributes.emotion;
        }
        else return default(Emotion);
    }
}
