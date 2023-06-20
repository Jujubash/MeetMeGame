using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class LoginMenuControllerScript : MonoBehaviourPunCallbacks
{
    private string _gameVersion = "1";
    private DatabaseScript _databaseScript;
    [SerializeField] private GameObject registerMenu;

    [SerializeField] private Text errorText;

    [SerializeField] private GameObject forgotPasswordMenu;

    [SerializeField] private InputField eMailInput;
    [SerializeField] private InputField passwordInput;
    
    private String _defaultRoomName = "MainRoom";
    [SerializeField] private byte maxPlayersPerRoom = 10;

    private void Awake()
    {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
            _databaseScript = new DatabaseScript("users");
            _databaseScript.Start();

    }
    
    // Start is called before the first frame update
    private void Start()
    {
        errorText.text = "";
    }

    public override void OnConnectedToMaster(){
        Debug.Log("001: Connected to MASTER");
        
        // PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayersPerRoom;
        PhotonNetwork.JoinOrCreateRoom(_defaultRoomName, roomOptions, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("001: Joined the ROOM: " + _defaultRoomName);

        PhotonNetwork.LoadLevel("MainRoom");
    }
    
    private void SetUserName()
    {
        string username = _databaseScript.GetUsername(eMailInput.text);
        PhotonNetwork.LocalPlayer.NickName = username;
    }

    private bool UserExists()
    {
        String userName = eMailInput.text;
        
        //TODO: Database call

        return true;
    }

    private bool PasswordIsCorrect()
    {
        String password = passwordInput.text;
        String email = eMailInput.text;
        if (_databaseScript.CheckLogin(email, password))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnLoginClicked()
    {
        if (PasswordIsCorrect())
        {   
            Debug.Log("password is correct");
            SetUserName();
            JoinRoom();
        }
        else
        {
            Debug.Log("falsche password or username!!");
            errorText.text = "Login credentials are incorrect!";
        }
    }

    public void OnRegisterClicked()
    {
        registerMenu.SetActive(true);
    }
    
    public void OnPasswordForgotClicked()
    {
        forgotPasswordMenu.SetActive(true);
    }
    
    
}