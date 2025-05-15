using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public SpawnLocation[] spawnLocations;
    public void SummonEnemy()
    {
        System.Random random = new System.Random();
        Instantiate(
            Resources.Load("Prefabs/Enemy") as GameObject,
            spawnLocations[random.Next(spawnLocations.Length)].transform.position,
            Quaternion.identity
        );
    }

    
}
