using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SolveInfoHUDBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_NbMoveText;
    [SerializeField] TextMeshProUGUI m_TimeText;

    [SerializeField] Graph m_Graph;

    private float m_StartTime;
    private bool m_IsTimerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Graph.OnInitGraph.AddListener(InitInfos);
        m_Graph.OnPendingGraphDesctruction.AddListener(ResetInfos);
        m_Graph.OnGraphCompletion.AddListener(StopTimer);
    }

    void InitInfos()
    {
        foreach(Node node in m_Graph.GetNodes())
            node.OnValueChanged.AddListener(UpdateNbMove);

        UpdateNbMove(0);
        m_StartTime = Time.time;
        m_IsTimerActive = true;
        StartCoroutine(Timer());
    }

    void ResetInfos()
    {
        foreach(Node node in m_Graph.GetNodes())
        {
            node.OnValueChanged.RemoveListener(UpdateNbMove);
        }
        m_StartTime = Time.time;
        StopTimer(0);
    }

    void UpdateNbMove(int iOldValue)
    {
        m_NbMoveText.text = m_Graph.GetNbMove().ToString();
    }

    void UpdateTime()
    {
        int totSeconds = Mathf.CeilToInt(Time.time - m_StartTime);
        int seconds;
        int totMinutes = Math.DivRem(totSeconds, 60, out seconds);
        int minutes;
        int totHours = Math.DivRem(totMinutes, 60, out minutes);

        string formatedTime = seconds.ToString("00");
        if (totSeconds >= 60)
            formatedTime = minutes.ToString("00") + ":" + formatedTime;
        if (totMinutes >= 60)
            formatedTime = totHours.ToString() + ":" + formatedTime;

        m_TimeText.text = formatedTime;
    }

    IEnumerator Timer()
    {
        while(m_IsTimerActive)
        {
            UpdateTime();

            yield return new WaitForSeconds(1);
        }
    }

    void StopTimer(int iTotalNbMove)
    {
        UpdateNbMove(iTotalNbMove);
        m_IsTimerActive = false;
        UpdateTime();
    }
}
