using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public PlayerWeapons _playerWeapons;
    private AudioSource _audioSource;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            int damage = _playerWeapons.FindWeapon("Lightning Wand").damage;
            col.GetComponent<AIMaster>().TakeDamage(damage);
        }
    }

    private void OnEnable()
    {
        _audioSource.Play();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
