using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Networking;

public class MyJobs : MonoBehaviour
{
    [SerializeField] private TMP_Text textMeshPro;
    
    private DatabaseScript _databaseScript;

    // Start is called before the first frame update
    void Start()
    {
        _databaseScript = new DatabaseScript("DBCollection");
        _databaseScript.Start();
        GetMyJobs();
    }

    void GetMyJobs()
    {   
        GameObject[] jobs = GameObject.FindGameObjectsWithTag("MyJob");
        Debug.Log("Job Objects " + jobs.Length);
        int i = 0;
        //Database call hier
        //get username:
        var results = _databaseScript.GetMyJobs(PhotonNetwork.LocalPlayer.NickName.ToString());
        
        
        Debug.Log("Results Count: " + results.Count);
        
        foreach (var result in results)
        {
            StringBuilder sb = new StringBuilder();
            string title = result["title"].ToString();
            string date = result["date"].ToString();
            string location = result["location"].ToString();
            string create_by = result["create_by"].ToString();
            sb.Append("Title: "+ title + Environment.NewLine);
            sb.Append("Date: "+ date + Environment.NewLine);
            sb.Append("Location: "+ location + Environment.NewLine);
            sb.Append("Created By: "+create_by + Environment.NewLine);
            // Set the TMP object text
            Debug.Log("Job from DB: " + sb.ToString());
            jobs[i].transform.Find("TextInfo").GetComponent<TextMeshProUGUI>().text = sb.ToString();

            i++;
        }
    }

}