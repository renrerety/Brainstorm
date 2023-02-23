using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text tmp;
    private float timer;

    private void Start()
    {
        tmp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float min = timer / 60;
        float sec = Mathf.Floor(timer % 60);

        tmp.text = String.Format("{0:00}:{1:00}", min, sec);
    }
}
