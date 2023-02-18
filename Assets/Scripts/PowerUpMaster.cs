using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpMaster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ApplyEffect();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public abstract void ApplyEffect();
}
