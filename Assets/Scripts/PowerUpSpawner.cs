using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PowerUp
{
    public int amountOfPowerUps;
    public GameObject[] typeOfPowerUps;
    public float spawnInterval;
}

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] PowerUp powerUps;
    public BoxCollider2D GridArea;
    private Vector2 spawnPosition;
    private bool canSpawn = true;
    private float nextSpawnTime;


    private void Start()
    {
        RandomPositionGenerator();
        SpawnPowerUp();
    }

    private void Update()
    {
        RandomPositionGenerator();
        SpawnPowerUp();
    }

    void SpawnPowerUp()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomPowerUp = powerUps.typeOfPowerUps[Random.Range(0, powerUps.typeOfPowerUps.Length)];
            Instantiate(randomPowerUp, spawnPosition, Quaternion.identity);
            powerUps.amountOfPowerUps--;
            nextSpawnTime = Time.time + powerUps.spawnInterval;
            if (powerUps.amountOfPowerUps == 0)
            {
                powerUps.amountOfPowerUps = 3;
            }
        }

    }

    private void RandomPositionGenerator()
    {
        Bounds bounds = this.GridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        spawnPosition = new Vector2(x, y);
    }
}
