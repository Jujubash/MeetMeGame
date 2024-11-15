﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Agora.Rtc;
using Agora.Util;
using Photon.Pun;
using Debug = UnityEngine.Debug;
using Logger = Agora.Util.Logger;
using Random = UnityEngine.Random;


    //Ricky changed
    public class JoinChannelVideo : MonoBehaviourPun
    {
        [FormerlySerializedAs("appIdInput")] [SerializeField]
        private AppIdInput _appIdInput;

        [Header("_____________Basic Configuration_____________")] [FormerlySerializedAs("APP_ID")] [SerializeField]
        private string _appID = "";

        [FormerlySerializedAs("TOKEN")] [SerializeField]
        private string _token = "";

        [FormerlySerializedAs("CHANNEL_NAME")] [SerializeField]
        private string _channelName = "";

        public Text LogText;
        internal Logger Log;
        internal IRtcEngine RtcEngine = null;

        //Ricky wrote
        public static int userCount = 0;

        // Use this for initialization
        public void Start()
        {
            userCount = 0;
            
            LoadAssetData();
            if (CheckAppId())
            {
                InitEngine();
                JoinChannel();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            PermissionHelper.RequestMicrophontPermission();
            PermissionHelper.RequestCameraPermission();
        }

        //Show data in AgoraBasicProfile
        [ContextMenu("ShowAgoraBasicProfileData")]
        private void LoadAssetData()
        {
            if (_appIdInput == null) return;

            _appID = _appIdInput.appID;
            /*
            _token = _appIdInput.token;
            _channelName = _appIdInput.channelName;
            */

            Dictionary<string, string> tokens =
                new Dictionary<string, string>();

            tokens.Add("Berlin",
                "007eJxTYJizKe1e6hQXuyVzlkk9kPtZfnCujPfJR/7MbuV8q6a5v5uuwJCaZGSZaphmlGaZZGRiZGGWlJRsZmFpaWGYZmpiaJJqoWh6KbkhkJHhu74cCyMDBIL4bAxOqUU5mXkMDACp3B/a");
            tokens.Add("Brandenburg", "007eJxTYLjFOLvu2RHN3w/WfV5ZeEMu0qkjqnaH7VzNZe2TF3joxeYqMKQmGVmmGqYZpVkmGZkYWZglJSWbWVhaWhimmZoYmqRaWJteSm4IZGTg27OSiZEBAkF8bganosS8lNS8pNKidAYGAKA6Isg=");
            tokens.Add("Hamburg", "007eJxTYNjtJr7aZUrzc1OGtA+FpzOEkjk3bfh2+cyr7mXRDuWGhjIKDKlJRpaphmlGaZZJRiZGFmZJSclmFpaWFoZppiaGJqkW/aaXkhsCGRnWSnSwMDJAIIjPzuCRmJtUWpTOwAAAligf/A==");
            tokens.Add("Baden-Wuerttemberg", "007eJxTYFi3ScJMZ60Oc/Y7o61LD9gV/Pgh/DEvaGJR7f9Z+8UffRdSYEhNMrJMNUwzSrNMMjIxsjBLSko2s7C0tDBMMzUxNEm1CDW7lNwQyMhwVcCYiZEBAkF8IQanxJTUPN3w0tSikpLU3KTUonQGBgBoByUY");
            tokens.Add("Bayern", "007eJxTYBCp8GifzWJeUiesvtdMN0XD7fMWsbrrxfWsU3qydHN8TyowpCYZWaYaphmlWSYZmRhZmCUlJZtZWFpaGKaZmhiapFr0mV1KbghkZKjkYWJiZIBAEJ+NwSmxMrUoj4EBAGWRHAs=");
            tokens.Add("Sachsen", "007eJxTYHi37dHvX//ea/Qd5V+UOWv3rKnTDn9wK27i2vtpWsGMwEcbFBhSk4wsUw3TjNIsk4xMjCzMkpKSzSwsLS0M00xNDE1SLdaaXUpuCGRkeCE1jZWRAQJBfHaG4MTkjOLUPAYGAFoIJVE=");
            tokens.Add("Saarland", "007eJxTYHjMVH3hutPCNZY6m+Nv1k51n6x/30XzMXvlRq6FSf6LMgMUGFKTjCxTDdOM0iyTjEyMLMySkpLNLCwtLQzTTE0MTVItdphdSm4IZGRouybEwAiFID4HQ3BiYlFOYl4KAwMAt/QgaA==");
            tokens.Add("Bremen", "007eJxTYChW6HwX+8r7bsSmapkV32oWMXsdjJt33vWhT5diqGbYp3kKDKlJRpaphmlGaZZJRiZGFmZJSclmFpaWFoZppiaGJqkWB80uJTcEMjKsV3FgYIRCEJ+NwakoNTc1j4EBAMGRIF8=");
            tokens.Add("Niedersachsen", "007eJxTYJC+WZrfYdn3qnfWuXmXP+44uTFs834GvtNtHr5/r8yLeyulwJCaZGSZaphmlGaZZGRiZGGWlJRsZmFpaWGYZmpiaJJqccrsUnJDICOD2W4hRkYGCATxeRn8MlNTUouKE5MzilPzGBgAZgElPQ==");
            tokens.Add("Nordrhein-Westfalen", "007eJxTYAitTOndvinkeFrPKrcVTz6kZcxR+XBoveobFZXLLS+Sl7soMKQmGVmmGqYZpVkmGZkYWZglJSWbWVhaWhimmZoYmqRa3DC7lNwQyMiQcsmeiZEBAkF8YQa//KKUoozUzDzd8NTikrTEnNQ8BgYAHzsnUw==");
            tokens.Add("Hessen", "007eJxTYDhfqL+WWbBn78HZqd9K+mTzI1pePlql5Sh0gIPlw4YjM5wUGFKTjCxTDdOM0iyTjEyMLMySkpLNLCwtLQzTTE0MTVIt3ptdSm4IZGR4GJTAzMgAgSA+G4NHanFxah4DAwCcWSCD");
            tokens.Add("Rheinland-Pfalz", "007eJxTYIiN+tG25HLO8S0LnW/8X93h3JHFuWTHX/vKZaYfHk+7M+GRAkNqkpFlqmGaUZplkpGJkYVZUlKymYWlpYVhmqmJoUmqxS+zS8kNgYwMZqbLmBgZIBDE52cIykjNzMtJzEvRDUhLzKliYAAAI2wnOQ==");
            tokens.Add("Thueringen", "007eJxTYPj+Qz9r8cm9p7u52LdsYV5Ww9qQlHyKr7gzaCtvRZ7O0X4FhtQkI8tUwzSjNMskIxMjC7OkpGQzC0tLC8M0UxNDk1QLQfNLyQ2BjAx71M4wMjJAIIjPxRCSUZpalJmXnprHwAAA/dghHQ==");
            tokens.Add("Sachsen-Anhalt", "007eJxTYFAK6KwJ1VzIZ+0SnPFv54LtjzuSTJ9X/P33SKfomIfK29UKDKlJRpaphmlGaZZJRiZGFmZJSclmFpaWFoZppiaGJqkWYeaXkhsCGRlitoszMTJAIIjPxxCcmJxRnJqn65iXkZhTwsAAAMjsI2k=");
            tokens.Add("Mecklenburg-Vorprommern", "007eJxTYMh3WRyya/1vew0mI35HFbndzso8PtdV+W78/XhfMXkFw1wFhtQkI8tUwzSjNMskIxMjC7OkpGQzC0tLC8M0UxNDk1SLJPNLyQ2BjAw359ozMEIhiC/O4JuanJ2TmpdUWpSuG5ZfVFCUn5ubWpTHwAAA+1Ekmg==");
            tokens.Add("Schleswig-Holstein", "007eJxTYFBSWbttD6vhzd3KqoahjGvnP1vHdtny8LIQLXULxeQVwSoKDKlJRpaphmlGaZZJRiZGFmZJSclmFpaWFoZppiaGJqkWeeaXkhsCGRn+1GszMTJAIIgvxBCcnJGTWlyema7rkZ9TXJKamcfAAAA1OiIa");
            
            string roomName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            _channelName = roomName;
            
            _token = tokens[roomName];
            /*
            AgoraTokenGenerator _agoraTokenGenerator = new AgoraTokenGenerator(_appID, "e1ff4b1a06f745329ed33bc097465c8f", _channelName, "1", PhotonNetwork.LocalPlayer.NickName, 3600);

            _token = _agoraTokenGenerator.GenerateDynamicKey();
            */
            
            Debug.Log("appID: " + _appID +  " ,channelName: " + _channelName + " ,userAccount:" + PhotonNetwork.LocalPlayer.NickName);
        }

        public void EnableCamera(bool enable)
        {
            RtcEngine.EnableLocalVideo(enable);
            //RtcEngine.EnableVideo();
        }

        public void EnableMic(bool enable)
        {
            RtcEngine.EnableLocalAudio(enable);
        }

        private bool CheckAppId()
        {
            Log = new Logger(LogText);
            return Log.DebugAssert(_appID.Length > 10,
                "Please fill in your appId in API-Example/profile/appIdInput.asset");
        }

        private void InitEngine()
        {
            RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
            UserEventHandler handler = new UserEventHandler(this);
            RtcEngineContext context = new RtcEngineContext(_appID, 0,
                CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
                AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
            RtcEngine.Initialize(context);
            RtcEngine.InitEventHandler(handler);
        }

        private void JoinChannel()
        {
            RtcEngine.EnableAudio();
            RtcEngine.EnableVideo();
            VideoEncoderConfiguration config = new VideoEncoderConfiguration();
            config.dimensions = new VideoDimensions(640, 360);
            config.frameRate = 15;
            config.bitrate = 0;
            RtcEngine.SetVideoEncoderConfiguration(config);
            RtcEngine.SetChannelProfile(CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_COMMUNICATION);
            RtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);

            //RtcEngine.JoinChannel(_token, _channelName);
            RtcEngine.JoinChannelWithUserAccount(_token, _channelName, PhotonNetwork.LocalPlayer.NickName);
            //RtcEngine.JoinChannel(_token, _channelName, "", 1);
        }

        public void OnDestroy()
        {
            Debug.Log("OnDestroy");

            if (RtcEngine == null) return;
            RtcEngine.InitEventHandler(null);
            RtcEngine.LeaveChannel();
            RtcEngine.Dispose();
            
            DestroyAllVideoSurfaces();
        }

        private void DestroyAllVideoSurfaces()
        {
            GameObject[] videoSurfaces = GameObject.FindGameObjectsWithTag("videoSurface");
            
            Debug.Log("Video Surfaces found:" + videoSurfaces.Length + "," + videoSurfaces.ToString());
            
            foreach (GameObject videoSurface in videoSurfaces)
            {
                Destroy(videoSurface);
            }
        }

        internal string GetChannelName()
        {
            return _channelName;
        }

        internal static void MakeVideoView(uint uid, string channelId = "")
        {
            var go = GameObject.Find(uid.ToString());
            if (!ReferenceEquals(go, null))
            {
                return; // reuse
            }


            // create a GameObject and assign to this new user
            var videoSurface = MakeImageSurface(uid.ToString());


            if (ReferenceEquals(videoSurface, null)) return;
            // configure videoSurface
            if (uid == 0)
            {
                videoSurface.SetForUser(uid, channelId);
            }
            else
            {
                videoSurface.SetForUser(uid, channelId, VIDEO_SOURCE_TYPE.VIDEO_SOURCE_REMOTE);
            }

            videoSurface.OnTextureSizeModify += (int width, int height) =>
            {
                //Ricky wrote
                float size = 0.9f;

                float scale = (float)height / (float)width;

                //Ricky changed
                videoSurface.transform.localScale = new Vector3(-size, size * scale, 1);
                Debug.Log("OnTextureSizeModify: " + width + "  " + height);
            };
            
            videoSurface.gameObject.tag = "videoSurface";
            
            videoSurface.SetEnable(true);
        }

        #region -- Video Render UI Logic ---

        // VIDEO TYPE 1: 3D Object
        private static VideoSurface MakePlaneSurface(string goName)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Plane);

            if (go == null)
            {
                return null;
            }

            go.name = goName;
            // set up transform
            go.transform.Rotate(-90.0f, 0.0f, 0.0f);
            var yPos = Random.Range(3.0f, 5.0f);
            var xPos = Random.Range(-2.0f, 2.0f);
            go.transform.position = Vector3.zero;
            go.transform.localScale = new Vector3(0.25f, 0.5f, 0.5f);

            // configure videoSurface
            var videoSurface = go.AddComponent<VideoSurface>();

            return videoSurface;
        }

        // Video TYPE 2: RawImage
        private static VideoSurface MakeImageSurface(string goName)
        {
            GameObject go = new GameObject();

            if (go == null)
            {
                return null;
            }

            go.name = goName;
            // to be renderered onto
            go.AddComponent<RawImage>();
            // make the object draggable
            go.AddComponent<UIElementDrag>();
            var canvas = GameObject.Find("VideoCanvas");
            if (canvas != null)
            {
                go.transform.parent = canvas.transform;
                Debug.Log("add video view");
            }
            else
            {
                Debug.Log("Canvas is null video view");
            }

            // set up transform
            go.transform.Rotate(0f, 0.0f, 180.0f);
            // Ricky changed
            // go.transform.localPosition = new Vector3(-137.6f, 79.8f, 0);

            //Ricky wrote

            Debug.Log("USERCOUNT :" + userCount);

            if (go.name.Equals("0"))
            {
                go.transform.localPosition = new Vector3(-130f, 75f, 0);
            }
            else
            {
                if (userCount == 1)
                    go.transform.localPosition = new Vector3(-30f, 75f, 0);
                else if (userCount == 2)
                    go.transform.localPosition = new Vector3(70f, 75f, 0);
                else if (userCount == 3)
                    go.transform.localPosition = new Vector3(-130f, -75f, 0);
                else if (userCount == 4)
                    go.transform.localPosition = new Vector3(-30f, -75f, 0);
                else if (userCount == 5)
                    go.transform.localPosition = new Vector3(70f, -75f, 0);
            }

            go.transform.localScale = new Vector3(2f, 3f, 1f);


            // configure videoSurface
            var videoSurface = go.AddComponent<VideoSurface>();

            userCount++;

            return videoSurface;
        }

        internal static void DestroyVideoView(uint uid)
        {
            var go = GameObject.Find(uid.ToString());
            if (!ReferenceEquals(go, null))
            {
                Destroy(go);
            }
        }

        # endregion
    }

    #region -- Agora Event ---

    internal class UserEventHandler : IRtcEngineEventHandler
    {
        private readonly JoinChannelVideo _videoSample;

        internal UserEventHandler(JoinChannelVideo videoSample)
        {
            _videoSample = videoSample;
        }

        public override void OnError(int err, string msg)
        {
            _videoSample.Log.UpdateLog(string.Format("OnError err: {0}, msg: {1}", err, msg));
        }

        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            int build = 0;
            Debug.Log("Agora: OnJoinChannelSuccess ");
            _videoSample.Log.UpdateLog(string.Format("sdk version: ${0}",
                _videoSample.RtcEngine.GetVersion(ref build)));
            _videoSample.Log.UpdateLog(string.Format("sdk build: ${0}",
                build));
            _videoSample.Log.UpdateLog(
                string.Format("OnJoinChannelSuccess channelName: {0}, uid: {1}, elapsed: {2}",
                    connection.channelId, connection.localUid, elapsed));

            JoinChannelVideo.MakeVideoView(0);
        }

        public override void OnRejoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            _videoSample.Log.UpdateLog("OnRejoinChannelSuccess");
        }

        public override void OnLeaveChannel(RtcConnection connection, RtcStats stats)
        {
            _videoSample.Log.UpdateLog("OnLeaveChannel");
            JoinChannelVideo.DestroyVideoView(0);
        }

        public override void OnClientRoleChanged(RtcConnection connection, CLIENT_ROLE_TYPE oldRole,
            CLIENT_ROLE_TYPE newRole)
        {
            _videoSample.Log.UpdateLog("OnClientRoleChanged");
        }

        public override void OnUserJoined(RtcConnection connection, uint uid, int elapsed)
        {
            _videoSample.Log.UpdateLog(string.Format("OnUserJoined uid: ${0} elapsed: ${1}", uid, elapsed));
            JoinChannelVideo.MakeVideoView(uid, _videoSample.GetChannelName());
            //JoinChannelVideo.userCount++;
        }

        public override void OnUserOffline(RtcConnection connection, uint uid, USER_OFFLINE_REASON_TYPE reason)
        {
            _videoSample.Log.UpdateLog(string.Format("OnUserOffLine uid: ${0}, reason: ${1}", uid,
                (int)reason));
            JoinChannelVideo.DestroyVideoView(uid);
            JoinChannelVideo.userCount--;
        }

        public override void OnUplinkNetworkInfoUpdated(UplinkNetworkInfo info)
        {
            _videoSample.Log.UpdateLog("OnUplinkNetworkInfoUpdated");
        }

        public override void OnDownlinkNetworkInfoUpdated(DownlinkNetworkInfo info)
        {
            _videoSample.Log.UpdateLog("OnDownlinkNetworkInfoUpdated");
        }
    }

    # endregion
