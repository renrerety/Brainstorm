using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/Walk Towards Player")]
public class WalkTowardsPlayer : ScriptableObject, IMovement
{
    public void Move(Transform enemy, Transform player,float speed)
    {
        enemy.position = Vector3.MoveTowards(enemy.position, player.position, Time.deltaTime * speed);
    }
}
