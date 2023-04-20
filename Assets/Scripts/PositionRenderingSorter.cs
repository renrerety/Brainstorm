using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRenderingSorter : MonoBehaviour
{
    //Script come from this tutorial : https://www.youtube.com/watch?v=CTf0WjhfBx8
    
    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField] private float offset = 0;
    [SerializeField] private bool runOnlyOnce = false;
    private float timer;
    private float timerMax = .1f;
    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y * 100 - offset);
            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
