using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Newtonsoft.Json;

public class RegisterUserScript : MonoBehaviour
{
    // URL of the API endpoint for registering a user
    private const string REGISTER_URL = "https://hoccungminh.dinhnt.com/fpt/register";

    // Input fields for the username and password
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError;

    // Register button
    public Button btRegis;


    private void Start()
    {
        // Attach the Register function to the Register button's onClick event
        btRegis.onClick.AddListener(Register);
    }

    private void Register()
    {
        // Get the username and password from the input fields
        var user = edtUser.text;
        var pass = edtPass.text;


        // Send the request to the API endpoint
        UserModel userModel = new UserModel(user, pass);

        StartCoroutine(SendRegisterRequest(userModel));
    }

    IEnumerator SendRegisterRequest(UserModel userModel)
    {

        // Create a JSON object with the username and password
        string jsonRequestBody = JsonConvert.SerializeObject(userModel);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // Create the HTTP request with the JSON body
        UnityWebRequest request = new UnityWebRequest(REGISTER_URL, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Convert the response from JSON to a RegisterResponseModel object
            string jsonString = request.downloadHandler.text;
            RegisterResponseModel registerResponse = JsonConvert.DeserializeObject<RegisterResponseModel>(jsonString);

            // Log the response status and message
            
            if (registerResponse.status == 1)
            {
                Debug.Log("Response status: " + registerResponse.status);
                Debug.Log("Response message: " + registerResponse.notification);
            }
            else
            {
                txtError.text = registerResponse.notification;
            }
        }
        request.Dispose();

    }
}
