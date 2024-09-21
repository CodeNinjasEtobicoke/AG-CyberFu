using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public float speed = 2f;
    public float attackingDistance = 0.6f;

    private Animator animatorEnemy;
    private Rigidbody rigidbodyEnemy;
    private Transform target;
    [SerializeField]
    private bool isFollowingTarget;

    private float currentAttackingTime = 0f;
    private float maxAttackingTime = 2f;

    // Start is called before the first frame update
    void Start()
    {

        animatorEnemy = GetComponent<Animator>();
        rigidbodyEnemy = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        isFollowingTarget = Vector3.Distance(transform.position, target.position) >= attackingDistance;
        if (isFollowingTarget)
        {
            rigidbodyEnemy.velocity = transform.forward * speed;
        }

        //If enemy's distance is close to attack the player

        else
        {
            Attack();
        }

        animatorEnemy.SetBool("Walk", isFollowingTarget);
    }

    void Attack()
    {
        rigidbodyEnemy.velocity = Vector3.zero;
        //the current attacking time add to the value of delta time

        currentAttackingTime += Time.deltaTime;

        if (currentAttackingTime > maxAttackingTime)
        {
            // set the current attacking time to 0 

            currentAttackingTime = 0f;
            int rand = Random.Range(1, 4);
            animatorEnemy.SetTrigger("Attack" + rand);
        }
    }
}
