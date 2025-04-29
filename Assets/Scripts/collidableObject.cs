using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    private Collider2D doorCollider;
    [SerializeField]
    private ContactFilter2D filter;
    private List<Collider2D> collisionList = new List<Collider2D>(1);
    public LogicScript logic;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        doorCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        doorCollider.OverlapCollider(filter, collisionList);
        //Debug.Log("Collision list: " + collisionList);
        foreach (var o in collisionList)
        {
            if (o.gameObject.tag == "Player")
            {
                OnCollided(o.gameObject);
                break;
            }
        }
    }

    protected virtual void OnCollided(GameObject collidedObject)
    {
        Debug.Log("Collided with " + collidedObject.name);
    }
}
