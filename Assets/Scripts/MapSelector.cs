using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    public static MapSelector instance;
    
    private StartGame _startGame;
    
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] public List<Map> mapList;
    [SerializeField] private Image img;
    [SerializeField] private Text label;
    public int index;
    
    private void Start()
    {
        LoadMapData();
    }
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
        if (index >= mapList.Count -1)
        {
            index = mapList.Count - 1;
        }
        UpdateDisplay();
    }

    public void Decrement()
    {
        index--;
        if (index < 0)
        {
            index = 0;
        }
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        img.sprite = mapList[index].img;
        label.text = mapList[index].label;
        StartGame.instance.scene = mapList[index].level;
        UpdateData();
    }

    public void LoadMapData()
    {
        if (PlayerData.instance.persistentData.iceMap)
        {
            mapList[1].unlocked = true;
        }
        UpdateData();
    }
    private void UpdateData()
    {
        foreach (var map in mapList)
        {
            if (map.unlocked)
            {
                map.img = map.unlockedSprite;
            }
            else
            {
                map.img = lockedSprite;
            }
        }
    }
}

[Serializable]
public class Map
{
    public Sprite img;
    
    public Sprite unlockedSprite;
    public string label;
    public LevelData level;
    public bool unlocked;

    public Map(Sprite img,string label,LevelData level, bool unlocked)
    {
        this.img = img;
        this.label = label;
        this.level = level;
        this.unlocked = unlocked;
    }
}
