using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public static Vector3 lastDirection = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
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
        Vector3 movement = new Vector3(horizontal * speed, vertical * speed, 0) * Time.deltaTime;
        transform.Translate(movement);
    }
}
