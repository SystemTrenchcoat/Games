using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public enum Action { Move, Attack };

    Entities entity;
    Rigidbody2D rb;
    Vector3 nextLocation;
    Vector3 playerLocation;

    public Action nextAction = Action.Move;
    public float actionDelay = 2f;
    public float timer = 2f;
    public float speed = .2f;

    public Tilemap dangers;
    public Tilemap barriers;

    public GameObject attack;
    public float xOffset = 0;
    public float yOffset = -1f;

    public int[] xs = {1,0,-1,0};
    public int[] ys = {0, 1, 0, -1};


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entities>();
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (timer <= 0)
        {
            //Debug.Log("Something");
            DecideAction();
            entity.isAttacking = false;

            if (nextAction == Action.Attack)
            {
                //Debug.Log("Attack");
                entity.isAttacking = true;
                //Debug.Log(xOffset + "\n" + yOffset);
                Instantiate(attack, new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0), Quaternion.identity);
                timer = actionDelay;
            }

            //gradually moves enemy to location
            else if (nextAction == Action.Move)
            {
                gameObject.transform.Translate(new Vector3(xOffset, yOffset, 0));
                //if (Vector3.Distance(transform.position, nextLocation) <= 0)
                    timer = actionDelay;
            }

            
        }

        //Debug.Log(timer);
        timer -= Time.deltaTime;
    }

    private void DecideAction()
    {
        Action act = Action.Move;

        bool skip = false;
        Vector3 check;
        Vector3 next;

        List<Vector3> locations = new List<Vector3>();

        //string[] directions = { "Down", "Up", "Right", "Left"};

        for (int i = 0; i < xs.Length; i++)
        {
            int x = xs[i];
            int y = ys[i];

            //Debug.Log(Mathf.Sign(x)+ " " + Mathf.Sign(y));
            check = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            next = new Vector3(transform.position.x + Math.Sign(x), transform.position.y + Math.Sign(y), 0);
            Vector3Int barrierMapTile = barriers.WorldToCell(next);

            //is next tile a wall? if no, return coordinate, if yes, return current position
            if (barriers.GetTile(barrierMapTile) == null)
            {
                //Debug.Log("No wall\n" + i);
                var collider = Physics2D.OverlapCircle(check, .5f);
                //is the next position occupied by anyone? if yes, player or enemy? if player, change to attack, if enemy, return current position, if neither, return next coordinate
                if (collider != null && collider.GetComponent<BoxCollider2D>() != null && collider.GetComponent<BoxCollider2D>())// != rb.GetComponent<BoxCollider2D>())
                {
                    //Debug.Log("Something near...");
                    if (collider.CompareTag("Player"))
                    {
                        if (check == next)
                        {
                            act = Action.Attack;
                            entity.changeDirection(i);
                            xOffset = Math.Sign(x);
                            yOffset = Math.Sign(y);
                            //i = 10; //end loop
                            //Debug.Log(xOffset + " " + yOffset);
                        }

                        else
                        {
                            act = Action.Move;
                            skip = true;
                            entity.changeDirection(i);
                            xOffset = Math.Sign(x);
                            yOffset = Math.Sign(y);
                            //i = 10; //end loop
                            //Debug.Log(xOffset + " " + yOffset);
                        }
                    }

                    else
                    {
                        next = new Vector3(transform.position.x, transform.position.y, 0);
                        //Debug.Log("Ally near\n" + collider);
                    }
                    //change direction to this direction and attack
                }

                else
                {
                    locations.Add(next);
                    //Debug.Log(next);
                }
            }

            else
            {
                next = new Vector3(transform.position.x, transform.position.y, 0);
                //Debug.Log("There's a wall");
            }
        }

        if (act == Action.Move && !skip)
        {
            if (locations.Count > 0)
            {
                //Debug.Log(xOffset + " " + yOffset);
                nextLocation = locations[Random.Range(0, locations.Count)];
                entity.changeDirection(locations.IndexOf(nextLocation));
                xOffset = nextLocation.x - transform.position.x;
                yOffset = nextLocation.y - transform.position.y;
            }
            else
            {
                nextLocation = new Vector3(transform.position.x, transform.position.y, 0);
                //Debug.Log(nextLocation);
            }
        }

        nextAction = act;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x + 2, transform.position.y + 0, 0), .3f);
        Gizmos.DrawSphere(new Vector3(transform.position.x + -2, transform.position.y + 0, 0), .3f);
        Gizmos.DrawSphere(new Vector3(transform.position.x + 0, transform.position.y + 2, 0), .3f);
        Gizmos.DrawSphere(new Vector3(transform.position.x + 0, transform.position.y + -2, 0), .3f);
    }
}
