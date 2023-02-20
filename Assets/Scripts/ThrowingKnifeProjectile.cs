using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ThrowingKnifeProjectile : MonoBehaviour
{
    [HideInInspector] public PlayerWeapons _playerWeapons;
    [HideInInspector] public ThrowingKnifePool _throwingKnifePool;
    
    public int damage;

    Transform player;

    [SerializeField] int speed;
    [SerializeField] int max_range;
    public int maxHit;
    int hit;
    
    private WeaponMaster throwingKnifeWeapon;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (WeaponMaster weapon in _playerWeapons.weapons)
        {
            throwingKnifeWeapon = weapon;
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
            _throwingKnifePool.ReturnThrowingKnifeToPool(gameObject);
        }

        if (hit >= throwingKnifeWeapon.maxHit)
        {
            _throwingKnifePool.ReturnThrowingKnifeToPool(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit < throwingKnifeWeapon.maxHit)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<AIMaster>().TakeDamage(throwingKnifeWeapon.damage);
                hit++;
            }
        }
    }
}
