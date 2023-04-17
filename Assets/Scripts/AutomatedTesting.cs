using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutomatedTesting : MonoBehaviour
{
    public static AutomatedTesting instance;
    
    private Transform player;

    public float timeScale = 1;
    public bool isTesting;
    public float speed;
    private float speedBuffer;

    private BoxCollider2D collider;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        collider = PlayerHealth.instance.gameObject.GetComponent<BoxCollider2D>();
    }
    public void StartAutomatedTesting()
    {
        speedBuffer = PlayerMovement.instance.speed;
        isTesting = true;
        collider.enabled = false;
        GameObject.Find("PickupZone").transform.localScale = new Vector3(200, 200, 200);
        GenerateRandomPos();
    }

    public void StopAutomatedTesting()
    {
        isTesting = false;
        PlayerMovement.instance.speed = speedBuffer;
        collider.enabled = true;
    }
    
    private Vector2 randomPos;
    public void GenerateRandomPos()
    {
        randomPos = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
    }
    private void Update()
    {
        if (isTesting)
        {
            Time.timeScale = timeScale;
            float posX = player.position.x;
            float posY = player.position.y;
            Vector2 playerPos = new Vector2(posX, posY);
            
            
            player.position = Vector2.MoveTowards(playerPos, randomPos, (speed/100)*Time.timeScale);
            
            if (Vector2.Distance(playerPos, randomPos) <= 0.5f)
            {
                GenerateRandomPos();
            }
        }
    }
}
