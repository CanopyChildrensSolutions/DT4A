using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SentimentRequest : MonoBehaviour
{

    public Texture2D TestImage;
    public Emotion Response;
    const string uriBase = "https://westus.api.cognitive.microsoft.com/face/v1.0/detect";


    public struct Emotion
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
            return base.ToString();

            return $"anger = {anger}\n" +
                $"contempt = {contempt}\n" +
                $"disgust = {disgust}\n" +
                $"happiness = {happiness}\n" +
                $"neutral = {neutral}\n" +
                $"sadness = {sadness}\n" +
                $"surprise = {surprise}\n";
        }
    }
    struct FaceAttributes
    {
        public Emotion emotion;
    }
    struct FaceInfo
    {
        public string faceId;
        public FaceAttributes faceAttributes;
    }

    public IEnumerator Upload(byte[] imageBytes)
    {

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

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
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
        var faces = JsonUtility.FromJson<FaceInfo[]>(json);
        if (faces != null && faces.Length != 0)
        {
            return faces[0].faceAttributes.emotion;
        }
        else return default(Emotion);
    }
}
