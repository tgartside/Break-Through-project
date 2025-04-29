using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnScript : MonoBehaviour
{
    public GameObject Projectile;
    public float spawnRate = 2;
    private float timer = 0;

    void Start()
    {
        SpawnProjectile();
    }
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnProjectile();
            timer = 0;
        }

    }

    void SpawnProjectile()
    {
        Instantiate(Projectile, new Vector2(transform.position.x, transform.position.y), transform.rotation);
    }
}
