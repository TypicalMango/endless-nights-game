using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    float timeLimit;
    float elapsedTime;
    float remainTime;
    public UnityEvent OnTimerZero;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (timeLimit > elapsedTime) {
            remainTime = timeLimit - (int)elapsedTime;
        }
        else {
            remainTime = 0;
            OnTimerZero.Invoke();
        }
        timerText.text = remainTime.ToString();
    }
}
