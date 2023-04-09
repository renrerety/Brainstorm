using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool iceMovement;
    public float speed;
    public static Vector3 lastDirection = new Vector3();

    [Inject] private DamagePopupPool _damagePopupPool;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private float speedModifier;
    private void Start()
    {
        speedModifier = 0;
        for (int i = 0; i < PlayerData.instance.persistentData.upgrades.speedUp; i++)
        {
            speedModifier += 0.05f;
        }

        speed += speedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        

        if(horizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(horizontal > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if(horizontal != 0 || vertical != 0)
        {
            GetComponent<Animator>().SetBool("IsMoving", true);
           
        }
        else
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
        }

        if(horizontal > 0)
        {
            lastDirection = new Vector3(0, Mathf.CeilToInt(horizontal), Mathf.CeilToInt(vertical));
        }
        else if (horizontal < 0)
        {
            lastDirection = new Vector3(0, Mathf.FloorToInt(horizontal), Mathf.FloorToInt(vertical));
        }
        if (vertical > 0)
        {
            lastDirection = new Vector3(0, Mathf.CeilToInt(horizontal), Mathf.CeilToInt(vertical));
        }
        else if (vertical < 0)
        {
            lastDirection = new Vector3(0, Mathf.FloorToInt(horizontal), Mathf.FloorToInt(vertical));
        }

        Vector3 movement = new Vector3(horizontal, vertical, 0);
        //movement = movement.normalized * Time.deltaTime * speed;
        
        movement = Vector3.ClampMagnitude(movement, 1f);
        movement *= Time.deltaTime * speed;
        //

        if (iceMovement)
        {
            speed = 15+speedModifier;
            rb.AddForce(movement);
        }
        else
        {
            speed = 3 + speedModifier;
            transform.Translate(movement);
        }
    }
}
