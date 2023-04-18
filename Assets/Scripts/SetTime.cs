using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private string SN;

    public void Start()
    {
        TimerText.text = PlayerPrefs.GetString(SN + "Timer");
    }
}
