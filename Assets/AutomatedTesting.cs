using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutomatedTesting : MonoBehaviour
{
    private int index;
    private Transform player;
    
    [SerializeField] private Vector2[] waypoints;

    public bool isTesting;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    public void StartAutomatedTesting()
    {
        isTesting = true;
        PlayerHealth.instance.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("PickupZone").transform.localScale = new Vector3(200, 200, 200);
        index = 0;
    }

    private void Update()
    {
        if (isTesting)
        {
            float posX = player.position.x;
            float posY = player.position.y;
            Vector2 playerPos = new Vector2(posX, posY);
            player.position = Vector2.MoveTowards(playerPos, waypoints[index], 0.025f);
            
            Debug.Log(index);

            if (Vector2.Distance(playerPos, waypoints[index]) <= 0.5f)
            {
                index++;
                if (index > waypoints.Length-1)
                {
                    index = 0;
                }
            }
        }
    }
}
