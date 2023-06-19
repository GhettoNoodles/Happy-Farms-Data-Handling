using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class AnimalManager : MonoBehaviour
{
    [SerializeField] public GameObject infoPanel;
    [SerializeField] private GameManager GM;
    [SerializeField] private TextMeshProUGUI txtHeader;
    [SerializeField] private TextMeshProUGUI txtAtt;
    [SerializeField] private TextMeshProUGUI txtHun;
    [SerializeField] private TextMeshProUGUI txtEn;
    [SerializeField] private TextMeshProUGUI txtCle;
    [SerializeField] private TextMeshProUGUI txtHealth;
    [SerializeField] private TextMeshProUGUI txtWAtt;
    [SerializeField] private TextMeshProUGUI txtWHun;
    [SerializeField] private TextMeshProUGUI txtWEn;
    [SerializeField] private TextMeshProUGUI txtWCle;
    [SerializeField] public Pen[] _pens = new Pen[4];
    public GameManager.Animal display;
    public int[] _timeSinceLastAtten = new int[4];
    public bool death = false;

    [SerializeField] public int[,] Stats =
    {
        { 100, 100, 100, 100, 100 }, { 100, 100, 100, 100, 100 }, { 100, 100, 100, 100, 100 },
        { 100, 100, 100, 100, 100 }
    };

    private void Start()
    {
    }

    public void UpdateAnimalStats(GameManager.Animal animal, int workersAtt, int workersHun, int workersEn,
        int workersCle)
    {
        int i = 0;
        switch (animal)
        {
            case GameManager.Animal.Pig:
                i = 0;
                break;
            case GameManager.Animal.Cow:
                i = 1;
                break;
            case GameManager.Animal.Chicken:
                i = 2;
                break;
            case GameManager.Animal.Fish:
                i = 3;
                break;
        }

        _pens[i].undoWarning();
        Stats[i, 0] = UpdateAttention(animal, workersAtt, Stats[i, 0]);
        Stats[i, 1] = UpdateHunger(animal, workersHun, Stats[i, 1]);
        Stats[i, 2] = UpdateEnergy(animal, workersEn, Stats[i, 2]);
        Stats[i, 3] = UpdateCleanliness(animal, workersCle, workersHun, Stats[i, 3]);
        Stats[i, 4] = UpdateHealth(animal, Stats[i, 4]);
    }

    private int UpdateAttention(GameManager.Animal animal, int workers, int att)
    {
        int attention = att;
        if (workers > 0)
        {
            _timeSinceLastAtten[(int)animal] = 0;
        }
        else
        {
            _timeSinceLastAtten[(int)animal]++;
        }

        attention = attention - 5 * (1 + _timeSinceLastAtten[(int)animal]) + 10 * workers;
        if (attention < 40)
        {
            _pens[(int)animal].Warning();
        }

        if (attention < 0)
        {
            return 0;
        }
        else if (attention > 100)
        {
            return 100;
        }
        else
        {
            return attention;
        }
    }

    private int UpdateHealth(GameManager.Animal animal, int hp)
    {
        int takesDamage = 0;
        int health = hp;
        for (int i = 0; i < 4; i++)
        {
            if (Stats[(int)animal, i] == 0)
            {
                takesDamage++;
            }
        }

        health -= takesDamage * 5;

        if (takesDamage > 0)
        {
            _pens[(int)animal].CodeRed();
        }
        else
        {
            health += 15;
        }

        if (health < 1)
        {
            death = true;
        }
        if (health > 100)
        {
            return 100;
        }
        else
        {
            return health;
        }
    }

    private int UpdateHunger(GameManager.Animal animal, int workers, int hun)
    {
        var hunger = hun;
        int energyFactor = 0;
        int energy = Stats[(int)animal, 2];
        if (energy < 100)
        {
            energyFactor++;
        }

        if (energy < 75)
        {
            energyFactor++;
        }

        if (energy < 50)
        {
            energyFactor++;
        }

        if (energy < 25)
        {
            energyFactor++;
        }

        hunger -= 3 * (1 + energyFactor);
        if (workers > 0)
        {
            hunger += 6 * workers;
            Stats[(int)animal, 2] += energyFactor * workers*2;
        }

        if (hunger < 40)
        {
            _pens[(int)animal].Warning();
        }
        if (hunger < 0)
        {
            return 0;
        }
        else if (hunger > 100)
        {
            return 100;
        }
        else
        {
            return hunger;
        }
    }

    private int UpdateEnergy(GameManager.Animal animal, int workers, int energy)
    {
        var supaC = energy;
        if (GM.timeofDay == GameManager.time.Day)
        {
            supaC -= 15;
            supaC += 5 * workers;
        }
        else
        {
            supaC += 5 * (1 + workers);
        }

        if (supaC < 40)
        {
            _pens[(int)animal].Warning();
        }

        if (supaC < 0)
        {
            return 0;
        }
        else if (supaC > 100)
        {
            return 100;
        }
        else
        {
            return supaC;
        }
    }

    private int UpdateCleanliness(GameManager.Animal animal, int workers, int hunWorkers, int cl)
    {
        var cleanliness = cl;
        cleanliness -= (5 +3*hunWorkers);
        cleanliness += 10 * workers;
        if (cleanliness < 40)
        {
            _pens[(int)animal].Warning();
        }
        if (cleanliness < 0)
        {
            return 0;
        }
        else if (cleanliness > 100)
        {
            return 100;
        }
        else
        {
            return cleanliness;
        }
    }

    private void Update()
    {
        if (infoPanel.gameObject.activeSelf)
        {
            int i = 0;
            switch (display)
            {
                case GameManager.Animal.Pig:
                    i = 0;
                    break;
                case GameManager.Animal.Cow:
                    i = 1;
                    break;
                case GameManager.Animal.Chicken:
                    i = 2;
                    break;
                case GameManager.Animal.Fish:
                    i = 3;
                    break;
            }

            txtHeader.text = display.ToString() + " Pen";
            txtAtt.text = Stats[i, 0].ToString();
            txtHun.text = Stats[i, 1].ToString();
            txtEn.text = Stats[i, 2].ToString();
            txtCle.text = Stats[i, 3].ToString();
            txtHealth.text = Stats[i, 4].ToString();
            txtWAtt.text = GM._workers[i, 0].ToString();
            txtWHun.text = GM._workers[i, 1].ToString();
            txtWEn.text = GM._workers[i, 2].ToString();
            txtWCle.text = GM._workers[i, 3].ToString();
        }
    }
}