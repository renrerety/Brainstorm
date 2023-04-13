using Cinemachine;
using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    public static WorldScrolling instance;
    //Script come from this tutorial : https://www.youtube.com/watch?v=m0Ik1K02xfo&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=2
    [SerializeField] public Transform player;
    Vector2Int currentTilePos = new Vector2Int(1,1);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition;
    float tileSize = 10;
    [SerializeField] GameObject[,] terrainTiles;

    int terrainHeight = 5;
    int terrainWidth = 5;

    int fovHeight = 5;
    int fovWidth = 5;

    public void Add(GameObject tileObj, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tileObj;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
        terrainTiles = new GameObject[terrainHeight,terrainWidth];
    }
    private void Start()
    {
        UpdateTilesOnScreen();
    }
    private void Update()
    {
        if (player)
        {
            playerTilePosition.x = (int)(player.position.x / tileSize);
            playerTilePosition.y = (int)(player.position.y / tileSize);

            playerTilePosition.x -= player.position.x < 0 ? 1 : 0;
            playerTilePosition.y -= player.position.y < 0 ? 1 : 0;

            if (currentTilePos != playerTilePosition)
            {
                currentTilePos = playerTilePosition;

                onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
                onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
                UpdateTilesOnScreen();
            }
        }
    }

    private void UpdateTilesOnScreen()
    {
        for(int pov_x = -(fovWidth/2); pov_x <= fovWidth / 2; pov_x++)
        {
            for (int pov_y = -(fovHeight/2); pov_y <= fovHeight / 2; pov_y++)
            {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x,playerTilePosition.y + pov_y);
            }
        }
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal)
    {
        if (horizontal)
        {
            if(currentValue >= 0)
            {
                currentValue = currentValue % terrainWidth;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainWidth - 1 +currentValue % terrainWidth;
            }
        }
        else
        {
            if (currentValue >= 0)
            {
                currentValue = currentValue % terrainHeight;
            }
            else
            {
                currentValue += 1;
                currentValue = terrainHeight - 1 + currentValue % terrainHeight;
            }
        }
        return (int)currentValue;
    }
}
