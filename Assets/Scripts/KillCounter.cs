using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public int killCount;
    private Text tmp;

    public void AddKill()
    {
        killCount++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        tmp.text = killCount.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<Text>();
    }
}
