using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class ClockDigital : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    private float timeStart;

    private string CurTime="";

    bool timerActive = true;

    [SerializeField] private TextMeshProUGUI TimerText;
    private string SN;
    [SerializeField]
    private Canvas myCanvasVictory;

    void Start()
    {
        TimeSpan time = TimeSpan.FromSeconds(timeStart);
        TimeText.text = time.ToString("hh':'mm':'ss");
        SN = SceneManager.GetActiveScene().name;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerActive) 
        {
            timeStart += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeStart);
            TimeText.text = time.ToString("hh':'mm':'ss");
            CurTime= time.ToString("hh':'mm':'ss");
        }
        
    }
    public void stopTimer()
    {
        timerActive = false;
    }
    public void startTimer()
    {
        timerActive = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stopTimer();
            myCanvasVictory.GetComponent<Canvas>().gameObject.SetActive(true);
            TimerText.text = CurTime;
            Time.timeScale = 0;
            Cursor.visible = true;
            SetTimer();
        }

    }
    public void SetTimer()
    {
        if (PlayerPrefs.GetString(SN + "Timer").Equals(""))
        {
            PlayerPrefs.SetString(SN + "Timer", CurTime);
            PlayerPrefs.Save();
        }
        else
        {
            if (TimeSpan.ParseExact(PlayerPrefs.GetString(SN + "Timer"),
                "h':'mm':'ss", CultureInfo.InvariantCulture)
                > TimeSpan.ParseExact(CurTime, "hh':'mm':'ss", CultureInfo.InvariantCulture))
            {
                PlayerPrefs.SetString(SN + "Timer", CurTime);
                PlayerPrefs.Save();
            }
        }

    }
}
