using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    [SerializeField] protected GameObject weakEnemy;
    [SerializeField] protected GameObject strongEnemy;
    public abstract GameObject CreateWeakEnemy();
    public abstract GameObject CreateStrongEnemy();
}
