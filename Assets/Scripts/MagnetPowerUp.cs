using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerUp : PowerUpMaster
{
    [SerializeField] private GameObject pickupZone;
    // Start is called before the first frame update
    void Start()
    {
        pickupZone = GameObject.Find("PickupZone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyEffect()
    {
        pickupZone.transform.localScale = new Vector3(
            pickupZone.transform.localScale.x + 5f,
            pickupZone.transform.localScale.y + 5f,
            pickupZone.transform.localScale.z + 5f);
    }
}
