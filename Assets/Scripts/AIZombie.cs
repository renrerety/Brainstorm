using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AIZombie : AIMaster
{
    public override void Update()
    {
        base.Update();
        movementStrategy.Move(gameObject.transform, player);
    }
    public override void Start()
    {
        base.Start();
        movementStrategy = new WalkTowardsPlayer();
    }
}
