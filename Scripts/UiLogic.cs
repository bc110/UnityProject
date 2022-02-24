using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLogic : MonoBehaviour
{
    public Text timerText;
    public Text deathText;
    public Text winText;
    public Text endTime;
    public GameObject endScreen;

    public int deathCount = 0;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timePast = Time.time - startTime;

        string minutes = ((int) timePast / 60).ToString();
        string seconds = ((int) timePast % 60).ToString();
        string milliSeconds = (((timePast % 60)-((int) timePast % 60))*100).ToString("f0");

        if(minutes.Length == 1) minutes = "0"+minutes;
        if(seconds.Length == 1) seconds = "0"+seconds;
        if(milliSeconds.Length == 1) milliSeconds = "0"+milliSeconds;
        if(milliSeconds.Length > 2) milliSeconds = "00";

        timerText.text = minutes + ":" + seconds+":"+milliSeconds ;
    }

    public void showEndScreen()
    {
        endScreen.GetComponent<Image>().enabled = true;

        float timePast = Time.time - startTime;

        string minutes = ((int) timePast / 60).ToString();
        string seconds = ((int) timePast % 60).ToString();
        string milliSeconds = (((timePast % 60)-((int) timePast % 60))*100).ToString("f0");

        if(minutes.Length == 1) minutes = "0"+minutes;
        if(seconds.Length == 1) seconds = "0"+seconds;
        if(milliSeconds.Length == 1) milliSeconds = "0"+milliSeconds;
        if(milliSeconds.Length > 2) milliSeconds = "00";
        
        winText.text = "You Win!";
        endTime.text = "Time : "+ minutes + ":" + seconds+":"+milliSeconds ;
        deathText.text = "Deaths : "+deathCount;

    }
}
