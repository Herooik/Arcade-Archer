using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersSpawnManager : MonoBehaviour
{
    [SerializeField] private float startPosX = -7f;
    [SerializeField] private float startPosY = 5;
    [SerializeField] private int enemiesToSpawnInRow = 10;
    [SerializeField] private List<GameObject> enemies;
    
    private int _enemiesToSpawnInColumn = 5;
    
    void Start()
    {
        _enemiesToSpawnInColumn = enemies.Count;
        for (int i = 0; i < _enemiesToSpawnInColumn; i++)
        {
            for (float j = 0; j < enemiesToSpawnInRow; j++)
            {
                var invader = Instantiate(enemies[i]);
                invader.name = "Invader " + j * i;
                invader.transform.position = new Vector3(j + startPosX, startPosY - i);
            }
        }
    }
}
