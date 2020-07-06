using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InvadersSpawnManager : MonoBehaviour
{
    public List<GameObject> spawnedEnemies;

    [SerializeField] private GameObject startSpawnPosition;
    [SerializeField] private int enemiesToSpawnInRow = 10;
    [SerializeField] private GameObject invaderContainer;
    [SerializeField] private List<GameObject> enemiesToUse;

    private int _enemiesToSpawnInColumn = 5;
    
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        _enemiesToSpawnInColumn = enemiesToUse.Count;

        for (int i = 0; i < _enemiesToSpawnInColumn; i++)
        {
            for (float j = 0; j < enemiesToSpawnInRow; j++)
            {
                var invader = Instantiate(enemiesToUse[i]);
                
                var newInvaderPos = new Vector2(startSpawnPosition.transform.position.x + j, startSpawnPosition.transform.position.y - i);
                invader.transform.position = newInvaderPos;
                invader.transform.SetParent(invaderContainer.transform);
                
                spawnedEnemies.Add(invader);
                invader.name = "Invader " + spawnedEnemies.Count;
                
                yield return new WaitForSeconds(0.1f);
            }
        }

        GetComponent<InvaderStepMovement>().enabled = true;
    }
}
