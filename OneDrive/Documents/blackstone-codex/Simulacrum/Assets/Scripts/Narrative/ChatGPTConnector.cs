using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class ChatGPTConnector : MonoBehaviour
{
    private string apiUrl = "https://api.openai.com/v1/chat/completions";
    private string apiKey = "YOUR_API_KEY_HERE";

    public IEnumerator SendDialogueRequest(string context, System.Action<string> callback)
    {
        string payload = JsonUtility.ToJson(new
        {
            model = "gpt-4",
            messages = new object[]
            {
                new { role = "system", content = "You are Vale, a wise and ethereal guide." },
                new { role = "user", content = context }
            }
        });

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(payload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("ChatGPTConnector: Request error: " + request.error);
            callback("I'm having trouble connecting to my memories.");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("ChatGPTConnector: Response received: " + jsonResponse);
            // TODO: Parse jsonResponse to extract the actual dialogue text.
            callback(jsonResponse);
        }
    }
}
