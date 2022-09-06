using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Damage : MonoBehaviour
{
    public enum Direction { Right, Up, Left, Down, UL, UR, DL, DR };
    public enum Type { Melee, Ranged };
    public enum Effect { None, Poison };
    public Entities attacker;

    public Direction direction;// = Direction.Down;
    public Type type;
    public Effect effect;

    public GameObject instanceCreated;
    public int instanceAmount;
    public string special;
    public float[] instancesXs;
    public float[] instancesYs;

    public int effectDamage;
    public float effectDuration;
    public float effectDamageCooldown;

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
        for (int i = 0; i < instanceAmount; i++)
        {
            //checks if there are any specific coordinates to put things
            if (instancesXs.Length > 0)
            {
                int e = i;
                //checks if i is too big to be in the list of xs
                //if so, subtracts by length until it is within range and makes offset whatever that number is
                while (e >= instancesXs.Length)
                {
                    e -= instancesXs.Length;
                }
                
                xOffset = instancesXs[e];
            }

            if (instancesYs.Length > 0)
            {
                int e = i;
                //checks if i is too big to be in the list of ys
                //if so, subtracts by length until it is within range and makes offset whatever that number is
                while (e >= instancesYs.Length)
                {
                    e -= instancesYs.Length;
                }

                yOffset = instancesYs[e];
            }

            if (special == "Trigger")
            {
                xOffset = xOffset * -1;
                yOffset = yOffset * -1;
            }

            if (special == "Weapon")
            {
                instanceCreated = attacker.weapon;
            }

            Instantiate(instanceCreated, new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, -1), Quaternion.identity);
        }

        if (special == "Trigger")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Scream");
        gameObject.transform.Translate(new Vector3(xOffset, yOffset, -1) * speed);
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
        //Debug.Log(collision);
        //findAttacker();
        if (collision.GetComponent<Entities>() != null && collision.GetComponent<Entities>() != attacker && dCount <= 0)
        {
            Entities entity = collision.GetComponent<Entities>();
            //Debug.Log(effect);
            if (entity.isFlying)
            {
                //Debug.Log("Uh oh");
                if (type != Type.Melee)
                {
                    entity.Damage(damage);
                }
            }
            else
            {
                //Debug.Log("hmmm");
                entity.Damage(damage);
            }

            if (effect == Effect.Poison)
            {
                //Debug.Log("Poisn");
                entity.inflictEffect((int)effect, effectDamage, effectDuration, effectDamageCooldown);
            }
            dCount = dCooldown;
            //Debug.Log(entity.health);
            if (special != "Trigger")
            {
                Destroy(this.gameObject);
            }
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
        else if (dir.Equals("UL"))
        {
            direction = Direction.UL;
            yOffset = 1;
            xOffset = -1;
        }
        else if (dir.Equals("UR"))
        {
            direction = Direction.UR;
            yOffset = 1;
            xOffset = 1;
        }
        else if (dir.Equals("DL"))
        {
            direction = Direction.DL;
            yOffset = -1;
            xOffset = -1;
        }
        else if (dir.Equals("DR"))
        {
            direction = Direction.DR;
            yOffset = -1;
            xOffset = 1;
        }
    }
}