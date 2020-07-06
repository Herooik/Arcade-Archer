using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderStepMovement : MonoBehaviour
{
    [SerializeField] private InvadersSpawnManager invadersList;
    [SerializeField][Range(0, 2)] private float pauseBetweenStep = 0.5f;
    [SerializeField][Range(0, 2)] private float stepDistance = 0.2f;
    [SerializeField][Range(0, 2)] private float changeColumnDistance = 0.5f;
    [SerializeField] private float rightBorderX = 7f;
    [SerializeField] private float leftBorderX = -7f;
    
    private bool moveInfinity = true;
    private bool _changeColumn;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(Moving());
        } while (moveInfinity);
    } 

    private IEnumerator Moving()
    {
        yield return new WaitForSeconds(pauseBetweenStep);

        MoveEnemy();

        yield return new WaitForSeconds(pauseBetweenStep);

        ChangeDirection();

        if (_changeColumn)
        {
            ChangeColumn();
            _changeColumn = false;
        }
    }
    
    private void MoveEnemy()
    {
        foreach (var enemy in invadersList.spawnedEnemies)
        {
            enemy.transform.position += Vector3.right * stepDistance;
        }
    }
    
    private void ChangeDirection()
    {
        foreach (var enemy in invadersList.spawnedEnemies)
        {
            if (enemy.transform.position.x >= rightBorderX)
            {
                stepDistance = -stepDistance;
                _changeColumn = true;
            }
            else if (enemy.transform.position.x <= leftBorderX)
            {
                stepDistance = -stepDistance;
                _changeColumn = true;
            }
        }
    }
    
    private void ChangeColumn()
    {
        foreach (var enemy in invadersList.spawnedEnemies) 
        {
            var newPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y - changeColumnDistance);
            enemy.transform.position = newPos;
        }
    }
}
