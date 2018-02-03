using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SentimentRequest : MonoBehaviour
{

    public Texture2D TestImage;

    const string uriBase = "https://westus.api.cognitive.microsoft.com/face/v1.0/detect";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Upload());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Upload()
    {
        byte[] myData = TestImage.EncodeToPNG();

        // Request parameters. A third optional parameter is "details".
        string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

        string uri = uriBase + "?" + requestParameters;

        WWWForm form = new WWWForm();
        form.AddBinaryData("data", myData);

        UnityWebRequest www = new UnityWebRequest(uri)
        {
            method = UnityWebRequest.kHttpVerbPOST,
            uploadHandler = new UploadHandlerRaw(myData)
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
        }
    }

}
