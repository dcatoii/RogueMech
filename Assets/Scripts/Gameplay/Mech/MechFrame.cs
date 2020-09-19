using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechFrame : Mob {

    public FrameCore Core;
    public FrameHead Head;
    public FrameArms Arms;
    public FrameLegs Legs;

    public Animator myAnimator;
    public float Speed = 5.0f;

    public Weapon RightHandWeapon;

    // Use this for initialization
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// The core takes full damage from every hit. If the core dies, it's GAME OVER MAN!
    /// Could later experiment with the core only taking overflow damage from other parts
    /// </summary>
    /// <param name="amount"></param>
    void CoreDamaged(int amount)
    {
        Core.TakeDamage(amount);
    }

    void LegsDamaged(int amount)
    {
        Legs.TakeDamage(amount);
        CoreDamaged(amount);
    }

    void HeadDamaged(int amount)
    {
        Head.TakeDamage(amount);
        CoreDamaged(amount);
    }

    void ArmsDamaged(int amount)
    {
        Arms.TakeDamage(amount);
        CoreDamaged(amount);
    }

    void CoreBroken()
    {
        Die();
    }

    void ArmsBroken()
    {

    }

    void LegsBroken()
    {

    }

    void HeadBroken()
    {

    }

    protected override void Die()
    {
        Mission.instance.PlayerFrame = null;
        Camera.main.transform.parent = null;
        Mission.instance.EndMission(false);
        //Cursor.lockState = CursorLockMode.None;
        base.Die();
    }

}
