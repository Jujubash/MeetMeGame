using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchJob : MonoBehaviour
{
    //[SerializeField] private GameObject joinBttn;
    //[SerializeField] private GameObject detailBttn;
    //[SerializeField] private TMP_Text textMeshPro;
    private DatabaseScript _databaseScript;
    [SerializeField] private GameObject jobsObject;

    private void Start()
    {
        _databaseScript = new DatabaseScript("DBCollection");
        _databaseScript.Start();
        ShowDataInPopup();
    }

    public void OnClickBack()
    {
        jobsObject.transform.Find("SearchJobsPopup").gameObject.SetActive(false);
        Debug.Log("ist gecklickt!!‚");
    }

    void ShowDataInPopup()
    {
        GameObject[] jobs = GameObject.FindGameObjectsWithTag("Job");
        
        Debug.Log("Job Objects " + jobs.Length);
        
        var results = _databaseScript.GetAllDataInDatabase();
        
        int i = 0;
        
        foreach (var result in results)
        {
            StringBuilder sb = new StringBuilder();

            string title = result["title"].ToString();
            string date = result["date"].ToString();
            string location = result["location"].ToString();
            string create_by = result["create_by"].ToString();
            string join_by = result["join_by"].ToString();
            
            sb.Append("Title: "+ title + Environment.NewLine);
            sb.Append("Date: "+ date + Environment.NewLine);
            sb.Append("Location: "+ location + Environment.NewLine);
            sb.Append("Created By: "+create_by + Environment.NewLine);
            sb.Append("Joined By: "+join_by + Environment.NewLine);
            // Set the TMP object text

            Debug.Log("Job from DB: " + sb.ToString());
            jobs[i].transform.Find("TextInfo").GetComponent<TextMeshProUGUI>().text = sb.ToString();

            i++;
        }
    }

    
}
