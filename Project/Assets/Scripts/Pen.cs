using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pen : MonoBehaviour
{
    [SerializeField] public Image img;
    [SerializeField] private GameManager.Animal info;
    [SerializeField] private AnimalManager AM;
    private bool warn = false;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HoverEnter()
    {
        var tempColor = img.color;
        tempColor.a = 0.4f;
        img.color = tempColor;
        
    }

    public void HoverExit()
    {
        if (warn == false)
        {
            var tempColor = img.color;
            tempColor.a = 0f;
            img.color = tempColor;
        }
    }

    public void Clicked()
    {
        AM.display = info;
        AM.infoPanel.gameObject.SetActive(true);
    }

    public void Warning()
    {
        var tempColor = Color.yellow;
        tempColor.a = 0.5f;
        img.color = tempColor;
        warn = true;
    }

    public void CodeRed()
    {
        var tempColor = Color.red;
        tempColor.a = 0.7f;
        img.color = tempColor;
        warn = true;
    }
    public void undoWarning()
    {
        var tempColor = Color.white;
        tempColor.a = 0f;
        img.color = tempColor;
        warn = false;
    }
}