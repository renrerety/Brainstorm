using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    public abstract void CreateWeakEnemy();
    public abstract void CreateStrongEnemy();
}
