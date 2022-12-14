
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstreoidSpawnner : MonoBehaviour
{
    [SerializeField] GameObject[] astreoids;
    [SerializeField] float astreoidSpawnCooldown= 1.5f;
    [SerializeField] Vector2 forceRange;

    float timer;
   public bool isGameOver = false;

    void Update()
    {
        if (isGameOver) { return; }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnAstreoid();
            timer += astreoidSpawnCooldown;
        }
    }

    Vector2 spawnPoint = Vector2.zero;
    Vector2 direction = Vector2.zero;

    private void SpawnAstreoid()
    {
        int side = UnityEngine.Random.Range(0,4);

        switch (side)
        {
            case 0:
                //Left
                AdjustValues(0,Random.value,1f, Random.Range(-1f, 1f));
                break;
            case 1:
                //Right
                AdjustValues(1, Random.value, -1f, Random.Range(-1f, 1f));
                break;
            case 2:
                //Bottom
                AdjustValues(Random.value, 0, Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                //Top
                AdjustValues(Random.value, 1, Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = Camera.main.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

       GameObject astreoidInstance = Instantiate( 
                    astreoids[Random.Range(0, astreoids.Length)],
                    worldSpawnPoint, 
                    Quaternion.Euler(0f,0f, Random.Range(0f,360f))
                    );
        Rigidbody astreoidRb = astreoidInstance.GetComponent<Rigidbody>();

        astreoidRb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
      
    }

    void AdjustValues(float xValue, float yValue, float xDirection, float yDirection)
    {
        spawnPoint.x = xValue;
        spawnPoint.y = yValue;
        direction = new Vector2(xDirection, yDirection);
    }

    
}
