using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

public class LoginUserScript : MonoBehaviour
{
    public TMP_InputField edtUser, edtPass;
    public TMP_Text txtError;
    public Selectable first;
    private EventSystem eventSystem;
    public Button btLogin;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        first.Select();
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKey(KeyCode.Return))
        {
            btLogin.onClick.Invoke();
        }
        //Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.
                currentSelectedGameObject.
                GetComponent<Selectable>().
                FindSelectableOnDown();
            if (next != null) next.Select();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Selectable next = eventSystem.
               currentSelectedGameObject.
               GetComponent<Selectable>().
               FindSelectableOnUp();
            if (next != null) next.Select();
        }
    }
    public void CheckLogin()
    {

        var user = edtUser.text;
        var pass = edtPass.text;

        UserModel userModel = new UserModel(user, pass);
        StartCoroutine(Login(userModel));
        Login(userModel);
        
        
        //goi API

    /*    if(user.Equals("mc") && pass.Equals("123"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            txtError.text = "Login Failed!"; 
        }*/
    }

    IEnumerator Login(UserModel userModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest); /*(doi Json sang kieu Byte)*/
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            LoginResponseModel loginrp = JsonConvert.DeserializeObject<LoginResponseModel>(jsonString);
            if(loginrp.status == 1)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                txtError.text = loginrp.notification;
            } 
                
        }
        request.Dispose();
    }


}
