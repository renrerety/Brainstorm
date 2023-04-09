using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapSelector : MonoBehaviour
{
    public static MapSelector instance;
    
    [Inject] public StartGame _startGame;
    
    [SerializeField] public List<Map> mapList;
    [SerializeField] private Image img;
    [SerializeField] private Text label;
    public int index;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }

    public void Increment()
    {
        index++;
        if (index == mapList.Count -1)
        {
            index = mapList.Count - 1;
        }
        UpdateData();
    }

    public void Decrement()
    {
        index--;
        if (index < 0)
        {
            index = 0;
        }
        UpdateData();
    }

    private void UpdateData()
    {
        img.sprite = mapList[index].img;
        label.text = mapList[index].label;
        _startGame.scene = mapList[index].level;
    }
    
}

[Serializable]
public class Map
{
    public Sprite img;
    public string label;
    public LevelData level;

    public Map()
    {
        this.img = img;
        this.label = label;
        this.level = level;
    }
}
