using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour {

    public List<Timer> allTimers = new List<Timer>();
    public float updateRate;
  
	// Use this for initialization
	void Start () {

        EnemyVariable.TimerCreated += AddTimer;
        EnemyVariable.EnemyDeadRemoveTimer += RemoveTimer;

        InvokeRepeating("UpdateTimers", 0, updateRate);
	}

    void UpdateTimers() {
        for (int i = 0; i < allTimers.Count; i++) {
            allTimers[i].UpdateTimer(updateRate);
        }
    }

    public void AddTimer(Timer timer) {
        allTimers.Add(timer);
    }

    public void RemoveTimer(Timer timer) {
        allTimers.Remove(timer);
    }
}

public class Timer {

    bool repeat;
    bool stopped;
    bool started;
    public float actualTime;
    public float maxTime;

    public delegate void OnTimerEnd();
    public event OnTimerEnd TimerEnded;


    public Timer(float tempoMassimo, bool ripetibile) {
        repeat = ripetibile;
        maxTime = tempoMassimo;
    }

    public void StartTimer() {
        started = true;
        stopped = false;
        actualTime = 0;
    }

    public void UpdateTimer(float updateTime) {
        if (!stopped) {
            actualTime += updateTime;
            if (actualTime >= maxTime && !stopped)
            {
                EndTimer();
            }
        }
           
    }

    public void StopTimer() {
        stopped = true;
    }

    public void EndTimer() {
        if (repeat)
            StartTimer();
        else
            StopTimer();

        TimerEnded();
    }

    public float GetTimer() {
        return actualTime;
    }

    public float GetMaxTimer()
    {
        return maxTime;
    }

}
