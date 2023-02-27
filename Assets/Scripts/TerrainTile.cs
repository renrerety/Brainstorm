using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    //Script come from this tutorial : https://www.youtube.com/watch?v=m0Ik1K02xfo&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=2
    [SerializeField] Vector2Int tilePosition;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);

        transform.position = new Vector3(-100, -100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
