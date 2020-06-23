using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStepMovement : MonoBehaviour
{
    [SerializeField] private InvadersSpawnManager list;
    [SerializeField] private float stepSpeed = 0.5f;
    [SerializeField] private float rightBorderX = 7f;
    [SerializeField] private float leftBorderX = -7f;
    
    private bool moveInfinity = true;
    private static float _speed = 1;
    private static bool _changeColumn;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(Moving());
        } while (moveInfinity);
    } 

    private IEnumerator Moving()
    {
        yield return new WaitForSeconds(stepSpeed);

        MoveEnemy();

        yield return new WaitForSeconds(stepSpeed);

        ChangeDirection();

        ChangeColumn();
    }
    
    private void MoveEnemy()
    {
        foreach (var enemy in list.spawnedEnemies)
        {
            enemy.transform.position += Vector3.right * _speed;
        }
    }
    
    private void ChangeDirection()
    {
        foreach (var enemy in list.spawnedEnemies)
        {
            if (enemy.transform.position.x >= rightBorderX)
            {
                _speed = -1;
                _changeColumn = true;
            }
            else if (enemy.transform.position.x <= leftBorderX)
            {
                _speed = 1;
                _changeColumn = true;
            }
        }
    }
    
    private void ChangeColumn()
    {
        if (_changeColumn)
        {
            foreach (var enemy in list.spawnedEnemies)
            {
                var newPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 1);
                enemy.transform.position = newPos;
            }
            _changeColumn = false;
        }
    }
}
