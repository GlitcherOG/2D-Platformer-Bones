using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum AIState
    {
        Patrol,
        Seek,
        Attack,
        Die
    }

    public float currHealth;
    public float maxHealth, moveSpeed, attackRange, attackSpeed, sightRange, baseDamage;
    public int damage = 1;

    [Header("Waypoints")]
    public int curWaypoint;
    public Transform waypointParent;
    protected Transform[] waypoints;

    [Header("Enemy Entity")]
    public GameObject self;
    public GameObject healthCanvas;
    public Transform player;
    public Animator anim;
    public AIState state;

    // Start is called before the first frame update
    void Start()
    {
        //get the waypoints
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*

    public void Seek()
    {
        //If player in sight range chase
        if (Vector3.Distance(player.position, self.transform.position) > sightRange || Vector3.Distance(player.position, self.transform.position) < attackRange || curHealth < 0)
        {
            return;
        }
        state = AIState.Seek;
        anim.SetBool("Run", true);
        agent.destination = player.position;
    }

    public virtual void Attack()
    {
        //if player in attack range attack

        if (Vector3.Distance(player.position, self.transform.position) >= attackRange || curHealth < 0 || player.GetComponent<PlayerHandler>().curHealth < 0)
        {
            return;
        }

        state = AIState.Attack;
        anim.SetTrigger("Attack");
        Debug.Log("Attack");


    }

    public void Die()
    {
        //if enemy <= 0 health death occurs
        if (curHealth > 0)
        {
            return;
        }
        //else we are dead so run this
        state = AIState.Die;
        agent.destination = self.transform.position;

        anim.SetTrigger("Die");
    }
    */
}
