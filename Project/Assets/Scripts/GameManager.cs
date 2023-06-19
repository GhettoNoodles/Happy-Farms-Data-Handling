using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[,] _workers = new int[4, 4];
    private float t = 0f;
    private int clock = 0;
    private int maxWorkers = 12;
    public int currentWorkers = 0;
    [SerializeField] private AnimalManager AM;
    [SerializeField] private Clock CLK;
    public time timeofDay = time.Day;
    [SerializeField] private GameObject GameOn;
    [SerializeField] private TextMeshProUGUI workersTotBusy;
    [SerializeField] private GameObject GameOver;

    public enum Animal
    {
        Pig,
        Cow,
        Chicken,
        Fish
    }

    public enum time
    {
        Day,
        Night
    }

    // Start is called before the first frame update
    void Start()
    {
        workersTotBusy.text = currentWorkers + "/" + maxWorkers;
    }

    // Update is called once per frame
    void Update()
    {
        if (AM.death == false)
        {
            t += Time.deltaTime;
            if (t >= 5f)
            {
                clock++;
                if (clock == 7)
                {
                    timeofDay = time.Night;
                }
                else if (clock == 1)
                {
                    timeofDay = time.Day;
                }

                if (clock == 12)
                {
                    clock = 0;
                }

                CLK.Tick(clock);
                for (int c = 0; c < 4; c++)
                {
                    Animal animal = (Animal)c;
                    AM.UpdateAnimalStats(animal, _workers[c, 0], _workers[c, 1], _workers[c, 2], _workers[c, 3]);
                }

                t = 0f;
            }
        }
        else
        {
            GameOn.gameObject.SetActive(false);
            GameOver.SetActive(true);
        }
    }

    public void Reset()
    {
        currentWorkers = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                _workers[i, j] = 0;
            }
            AM._pens[i].undoWarning();
            AM._timeSinceLastAtten[i] = 0;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                AM.Stats[i, j] = 100;
            }
        }
        workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        
        clock = 0;
        t = 0f;
        AM.death = false;
        GameOn.gameObject.SetActive(true);
        GameOver.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void AddWAtt()
    {
        var i = (int)AM.display;
        if (currentWorkers < maxWorkers)
        {
            _workers[i, 0]++;
            currentWorkers++;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void AddWHun()
    {
        var i = (int)AM.display;
        if (currentWorkers < maxWorkers)
        {
            _workers[i, 1]++;
            currentWorkers++;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void AddWEn()
    {
        var i = (int)AM.display;
        if (currentWorkers < maxWorkers)
        {
            _workers[i, 2]++;
            currentWorkers++;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void AddWCl()
    {
        var i = (int)AM.display;
        if (currentWorkers < maxWorkers)
        {
            _workers[i, 3]++;
            currentWorkers++;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void RemWAtt()
    {
        var i = (int)AM.display;
        if (_workers[i, 0] > 0)
        {
            _workers[i, 0]--;
            currentWorkers--;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void RemWHun()
    {
        var i = (int)AM.display;
        if (_workers[i, 1] > 0)
        {
            _workers[i, 1]--;
            currentWorkers--;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void RemWEn()
    {
        var i = (int)AM.display;
        if (_workers[i, 2] > 0)
        {
            _workers[i, 2]--;
            currentWorkers--;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }

    public void RemWCl()
    {
        var i = (int)AM.display;
        if (_workers[i, 3] > 0)
        {
            _workers[i, 3]--;
            currentWorkers--;
            workersTotBusy.text = currentWorkers + "/" + maxWorkers;
        }
    }
}