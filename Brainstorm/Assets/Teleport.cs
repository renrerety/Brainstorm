using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] Axis axis;
    [SerializeField] Tilemap tilemap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            /*Vector3 pos = new Vector3();
            if (axis == Axis.x)
            {
                pos = collision.transform.position;
                pos.x = destination.position.x;
            }
            else if (axis == Axis.y)
            {
                pos = collision.transform.position;
                pos.y = destination.position.y;
            }
            collision.transform.position = pos;*/
            tilemap.transform.position = collision.transform.position;
        }
    }
}
enum Axis
{
    x,
    y
}
