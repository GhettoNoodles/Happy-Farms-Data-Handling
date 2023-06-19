using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    [SerializeField] private Image img;
    // Start is called before the first frame update
    public void Tick(int time)
    {
        img.sprite = sprites[time];
    }
}
