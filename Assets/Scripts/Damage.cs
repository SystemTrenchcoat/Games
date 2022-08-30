using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right };
    public GameObject attacker;

    public Direction direction;// = Direction.Down;
    public int damage;// = 5;
    public float cooldown;// = 1f;
    public float count;// = 1f;
    public float speed;// = .2f;
    public float xOffset;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Scream");
        gameObject.transform.Translate(new Vector3(xOffset, yOffset, 0) * speed);
        //Debug.Log(xOffset + "\n" + yOffset);
        if (count <=  0)
        {
            Destroy(this.gameObject);
        }
        count -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        //findAttacker();
        if (collision.GetComponent<Entities>() != null && collision.GetComponent<GameObject>() != attacker)
        {
            Debug.Log("Hit " + collision);
            Entities entity = collision.GetComponent<Entities>();
            entity.Damage(damage);
        }
    }

    private void findAttacker()
    {
        Entities attack = null;
        Entities[] entities = FindObjectsOfType<Entities>();
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Entities potentialAttacker in entities)
        {
            Transform potentialPosition = potentialAttacker.GetComponent<Transform>();
            Vector3 directionToTarget = potentialPosition.position - currentPosition;
            float dSqrToAttacker = directionToTarget.sqrMagnitude;
            if (potentialAttacker.isAttacking)
            {
                if (dSqrToAttacker < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToAttacker;
                    attack = potentialAttacker;
                    attacker = attack.GetComponent<GameObject>();
                    Debug.Log(attack.direction.ToString());
                    changeDirection(attack.direction.ToString());
                    Debug.Log(attack);
                }
            }
        }
    }

    private void changeDirection(string dir)
    {
        if (dir.Equals("Up"))
        {
            direction = Direction.Up;
            yOffset = 1;
            xOffset = 0;
        }
        else if (dir.Equals("Down"))
        {
            direction = Direction.Down;
            yOffset = -1;
            xOffset = 0;
        }
        else if (dir.Equals("Right"))
        {
            direction = Direction.Right;
            yOffset = 0;
            xOffset = 1;
        }
        else if (dir.Equals("Left"))
        {
            direction = Direction.Left;
            yOffset = 0;
            xOffset = -1;
        }
    }
}
