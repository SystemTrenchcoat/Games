using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right };
    public enum Type { Melee, Ranged };
    public Entities attacker;

    public Direction direction;// = Direction.Down;
    public Type type;
    public int damage;// = 5;
    public float dCooldown;
    public float dCount;
    public float cooldown;// = 1f;
    public float count;// = 1f;
    public float speed;// = .2f;
    public float xOffset;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        //changeDirection(attacker.GetComponent<Entities>().direction.ToString());
        //Debug.Log(direction);
        //Debug.Log(attacker.GetComponent<Entities>().direction);
        findAttacker();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Scream");
        gameObject.transform.Translate(new Vector3(xOffset, yOffset, 0) * speed);
        //Debug.Log(xOffset + "\n" + yOffset);
        if (count <= 0)
        {
            Destroy(this.gameObject);
        }
        count -= Time.deltaTime;
        dCount -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        //findAttacker();
        if (collision.GetComponent<Entities>() != null && collision.GetComponent<Entities>() != attacker && dCount <= 0)
        {
            Entities entity = collision.GetComponent<Entities>();
            entity.Damage(damage);
            dCount = dCooldown;
            Debug.Log(entity.health);
        }

        Destroy(this.gameObject);
    }

    private void findAttacker()
    {
        Entities attack = null;
        Entities[] entities = FindObjectsOfType<Entities>();
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Entities potentialAttacker in entities)
        {
            //Debug.Log(potentialAttacker);
            Transform potentialPosition = potentialAttacker.GetComponent<Transform>();
            Vector3 directionToTarget = potentialPosition.position - currentPosition;
            float dSqrToAttacker = directionToTarget.sqrMagnitude;
            if (potentialAttacker.isAttacking)
            {
                if (dSqrToAttacker < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToAttacker;
                    attack = potentialAttacker;
                    attacker = attack;
                    //Debug.Log(attack.GetComponent<Transform>());
                    changeDirection(attacker.direction.ToString());
                    //Debug.Log(attack);
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