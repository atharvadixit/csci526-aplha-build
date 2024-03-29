using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; // Added to use UnityWebRequest

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL;
    private long _sessionID;
    /*private float x_coord;
    private int level;*/

    private void Awake()
    {
        _sessionID = System.DateTime.Now.Ticks;
    }

    public void Send(float x_coord, string level)
    {
        /*x_coord = Random.Range(0, 101);*/     // x coord of the player at the time of death
        /*level = 0;*/                          // level at the time of death
        x_coord = (float)System.Math.Floor(x_coord);
        char levelNum = level[level.Length - 1];
        StartCoroutine(Post(_sessionID.ToString(), x_coord.ToString(), levelNum.ToString()));
        Debug.Log("Sendinggg Daa-taa" + levelNum);
    }

    private IEnumerator Post(string _sessionID, string x_coord, string level)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1514008209", _sessionID);
        form.AddField("entry.835408423", x_coord);
        form.AddField("entry.1069011455", level);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete");
            }
        }
    }
}
