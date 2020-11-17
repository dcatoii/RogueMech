using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BZard : Mob {
    public int Health = 300;
    public Weapon beam;
    public float Speed = 15.0f;
    public float LiftSpeed = 7.5f;
    public float hoverHeight = 25.0f;
    public float OrbitSpeed = 180.0f;
    public float TurnSpeed = 180.0f;
    public Mob Target;
    public float OrbitDistance;
    public float OrbitDistanceSq { get { return OrbitDistance * OrbitDistance; } }
    public Vector3 targetLastPosition;


    static Dictionary<Mob, BZard> AttachedBzards = new Dictionary<Mob, BZard>();

    enum BZARDState
    {
        Chase,
        Orbit,
        Charge,
        Fire,
        Dying,
    }

    BZARDState currentState;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapons"))
        {
            Projectile colProjectile = collision.gameObject.GetComponent<Projectile>();
            if (colProjectile.Source == this)
                return;

            Health -= colProjectile.Damage;
            if (Health <= 0)
            {
                currentState = BZARDState.Dying;
                //let physics take over
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                //bop it!
                //TODO: Try sending in the direction opposite the collision point instead
                float randForceX = UnityEngine.Random.Range(0.0f, 10.0f);
                float randForceY = UnityEngine.Random.Range(0.0f, 5.0f);
                float randForceZ = UnityEngine.Random.Range(-3.0f, -10.0f);
                GetComponent<Rigidbody>().AddForce(randForceX,randForceY,randForceZ, ForceMode.Impulse);

            }
        }
        //when dying, crash when we hit the ground
        else if (currentState == BZARDState.Dying && collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        //dying units do nothing
        if (currentState == BZARDState.Dying)
            return;
        //if I have no target, look for one
        if(Target == null)
        {
            //TODO: Use tracker system
            Target = Mission.instance.PlayerFrame;
            currentState = BZARDState.Chase;
        }
        //if we still don't have a target, just chill
        if (Target == null)
        {
            Idle();
            return;
        }
        //if my target is not in range, chase them
        if (currentState == BZARDState.Chase)
            ChaseTarget();

        //If I am close to my target, 
        else if (currentState == BZARDState.Orbit)
        {
            //and my weapon is ready, begin charging my weapon
            //Otherwise If my weapon is on cooldown, orbit my target
            RotateAroundTarget();
        }
        //If my weapon is not firing, adjust my height

    }

    private void Idle()
    {
        //bob up and down
        transform.position += new Vector3(0.0f, Mathf.Sin(Time.unscaledTime), 0.0f);

    }

    void TurnTowardsTarget()
    {
        //get the angle between look and target
        /*
        Vector3 toTarget = (Target.transform.position - transform.position).normalized;
        float fAngle = Vector3.Angle(transform.forward, toTarget);

        Vector3 target = Mission.instance.PlayerFrame.gameObject.transform.position;
        float strength = .5f;

        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        float str = Mathf.Min(strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);*/
        
        // Determine which direction to rotate towards
        Vector3 targetDirection = Target.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = TurnSpeed * Time.fixedDeltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        // Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

    }

    void RotateAroundTarget()
    {
        //keep up with target
        Vector3 targetMove = Target.transform.position - targetLastPosition;
        targetMove.y = 0;
        Vector3 MaxPotentialMovement = targetMove.normalized * Speed * Time.fixedDeltaTime;
        if (targetMove.sqrMagnitude < MaxPotentialMovement.sqrMagnitude)
            transform.position += targetMove;
        else
            transform.position += MaxPotentialMovement;

        //keep up with target y
        ChaseTargetY();

        if ((Target.transform.position - transform.position).sqrMagnitude > OrbitDistanceSq)
        {
            currentState = BZARDState.Chase;
            return;
        }

        Vector3 rotateOirigin = Mission.instance.PlayerFrame.gameObject.transform.position;
        //spin around the target unit
        transform.RotateAround(rotateOirigin, Vector3.up, OrbitSpeed * Time.fixedDeltaTime);
        transform.LookAt(Mission.instance.PlayerFrame.gameObject.transform);

        targetLastPosition = Target.transform.position;
    }

    void ChaseTarget()
    {
        //try to face the right direction
        TurnTowardsTarget();
        
        //get an x/z forward vector
        Vector3 move = transform.forward;
        move.y = 0;
        move.Normalize();

        //move the Mob
        transform.position += move * Time.fixedDeltaTime * Speed;

        //chase on the Y axis
        ChaseTargetY();

        //if my target in range, move to orbit
        if ((Target.transform.position - transform.position).sqrMagnitude <= OrbitDistanceSq)
        {
            currentState = BZARDState.Orbit;
            targetLastPosition = Target.transform.position;
        }

    }

    void ChaseTargetY()
    {
        Vector3 movement = new Vector3(0.0f, LiftSpeed * Time.fixedDeltaTime, 0.0f);
        if ((Target.transform.position.y + hoverHeight )> transform.position.y )
            transform.position += movement;
        else
            transform.position -= movement;

    }

}
