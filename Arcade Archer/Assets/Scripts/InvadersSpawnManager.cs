using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersSpawnManager : MonoBehaviour
{
    public List<GameObject> spawnedEnemies;
    
    [SerializeField] private float startPosX = -7f;
    [SerializeField] private float startPosY = 5;
    [SerializeField] private int enemiesToSpawnInRow = 10;
    [SerializeField] private List<GameObject> enemiesToUse;

    private int _enemiesToSpawnInColumn = 5;
    
    void Start()
    {
        _enemiesToSpawnInColumn = enemiesToUse.Count;
        for (int i = 0; i < _enemiesToSpawnInColumn; i++)
        {
            for (float j = 0; j < enemiesToSpawnInRow; j++)
            {
                var invader = Instantiate(enemiesToUse[i]);
                invader.name = "Invader " + j;
                invader.transform.position = new Vector3(j + startPosX, startPosY - i);
                
                spawnedEnemies.Add(invader);
            }
        }
    }
}
