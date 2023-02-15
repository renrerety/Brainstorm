using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FireballProjectile : MonoBehaviour
{
    public int damage;

    Transform player;

    [SerializeField] private AudioClip fireballClip;
    [SerializeField] int speed;
    [SerializeField] int max_range;
    public int maxHit;

    private WeaponMaster fireballWeapon;

    int hit;


    private void OnEnable()
    {
        GetComponent<AudioSource>().PlayOneShot(fireballClip);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        foreach (WeaponMaster weapon in PlayerWeapons.Instance.weapons)
        {
            if (weapon.name == "Fireball")
            {
                fireballWeapon = weapon;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += (transform.right * speed) * Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) > max_range)
        {
            FireballPool.Instance.ReturnFireballToPool(gameObject);
        }

        if (hit >= fireballWeapon.maxHit)
        {
           
            FireballPool.Instance.ReturnFireballToPool(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit < fireballWeapon.maxHit)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<AIMaster>().TakeDamage(fireballWeapon.damage);
                hit++;
            }
        }
    }
}
