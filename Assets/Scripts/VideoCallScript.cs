using UnityEngine;
using UnityEngine.UI;

public class VideoCallScript : MonoBehaviour
{
    [SerializeField] private GameObject videoCanvas;

    [SerializeField] private GameObject videoLoadingInfo;

    [SerializeField] private GameObject videoKeysInfo;

    [SerializeField] private JoinChannelVideo joinChannelVideo;

    [SerializeField] private bool camEnabled = true;

    [SerializeField] private bool micEnabled = true;

    [SerializeField] private GameObject micDisabledIcon;
    
    [SerializeField] private GameObject micEnabledIcon;
    
    [SerializeField] private GameObject camDisabledIcon;
    
    [SerializeField] private GameObject camEnabledIcon;

    private Texture _tempTexture;

    public void EnableVideoCall()
    {
        videoLoadingInfo.SetActive(true);

        videoCanvas.GetComponent<Canvas>().enabled = false;

        Invoke("InitializeVideoCanvas", 1f);
    }

    public void InitializeVideoCanvas()
    {
        videoCanvas.SetActive(true);
        joinChannelVideo.Start();
        
        Invoke("EnableVideoCanvas", 2f);
    }

    public void DisableVideoCall()
    {
        videoLoadingInfo.SetActive(false);

        videoKeysInfo.SetActive(false);

        joinChannelVideo.OnDestroy();

        videoCanvas.SetActive(false);
        
        camEnabledIcon.SetActive(false);
        micEnabledIcon.SetActive(false);
        
        camDisabledIcon.SetActive(false);
        micDisabledIcon.SetActive(false);
    }

    private void EnableVideoCanvas()
    {
        videoCanvas.GetComponent<Canvas>().enabled = true;

        videoKeysInfo.SetActive(true);
        
        camEnabledIcon.SetActive(true);
        micEnabledIcon.SetActive(true);
        
        camDisabledIcon.SetActive(false);
        micDisabledIcon.SetActive(false);
        
        videoLoadingInfo.SetActive(false);
    }

    public bool VideoCanvasIsActive()
    {
        return videoCanvas.activeSelf;
    }

    public void ToggleCamera()
    {
        camEnabled = !camEnabled;
        
        camDisabledIcon.SetActive(!camEnabled);
        
        camEnabledIcon.SetActive(camEnabled);

        joinChannelVideo.EnableCamera(camEnabled);

        if (!camEnabled)
        {
            _tempTexture = GameObject.Find("0").GetComponent<RawImage>().texture;

            DeleteCamTexture();
        }
        else
        {
            LoadCamTexture();
        }
    }

    public void ToogleMic()
    {
        micEnabled = !micEnabled;
        
        micDisabledIcon.SetActive(!micEnabled);
        
        micEnabledIcon.SetActive(micEnabled);

        joinChannelVideo.EnableMic(micEnabled);
    }

    private void DeleteCamTexture()
    {
        GameObject.Find("0").GetComponent<RawImage>().texture = null;
    }

    private void LoadCamTexture()
    {
        GameObject.Find("0").GetComponent<RawImage>().texture = _tempTexture;
    }
}