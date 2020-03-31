using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager manager;
    public TextMeshProUGUI timerText;
    
    public float time;
    bool isCounting = false;

    void Start()
    {
        Invoke("StartTimer", 3);
    }

    void Update()
    {
        if(isCounting)
            time -= Time.deltaTime;  

        if(timerText != null)
            timerText.text = time.ToString("F2").Replace(".", ":");

        if (time < 0)
        {
            time = 0;
            isCounting = false;
            manager.EndGame(false);
        }
    }

    void StartTimer()
    {
        isCounting = true;
    }
}
