using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteGround : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tilemap.transform.position = collision.transform.position;
        }
    }
}
