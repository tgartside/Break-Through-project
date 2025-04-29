using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveScript : MonoBehaviour
{
    public float moveSpeed = -5;
    public float deadZone = 10;
    public PlayerScript player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x > deadZone)
        {
            Debug.Log("Projectile Deleted");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.playerIsAlive)
        {
            player.playerDeath();
        }
    }
}
