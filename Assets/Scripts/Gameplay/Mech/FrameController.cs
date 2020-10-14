using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour {

    public MechFrame ControlledFrame;

    public float CoreRotX = 0.0f, CoreRotY = 0.0f;
    public float LegRot = 0.0f;

    [Range(0, 1f)]
    [SerializeField]
    float verticalSwayAmount = 0.5f;

    [Range(0, 1f)]
    [SerializeField]
    float horiztonalSwayAmount = 1f;

    [Range(0, 15f)]
    [SerializeField]
    float swaySpeed = 0.5f;

    public void MoveForward(float fMagntude)
    {
        if(fMagntude == 0.0f)
        {
            ControlledFrame.myAnimator.SetBool("Walking", false);
            ControlledFrame.Core.thruster.ToggleFwdThruster(false);
            return;
        }

        Vector3 transformVector = gameObject.transform.forward;
        transformVector.y = 0;
        transformVector.Normalize();
        //half leg speed if legs are destroyed
        float LegSpeed = ControlledFrame.Legs.IsDestroyed ? (ControlledFrame.Legs.Speed / 2) : ControlledFrame.Legs.Speed;
        float speedMultiplier = LegSpeed + ControlledFrame.Core.thruster.ThrusterSpeed;
        gameObject.transform.localPosition += transformVector * Time.fixedDeltaTime * fMagntude * speedMultiplier;
        ControlledFrame.myAnimator.SetBool("Walking", true);

        //Leg turning
        float absRotationDelta = Mathf.Clamp(ControlledFrame.Legs.TurnSpeed * Time.fixedDeltaTime, 0.0f, Mathf.Abs(CoreRotX));
        absRotationDelta = (CoreRotX < 0.0f ? -absRotationDelta : absRotationDelta);
        LegRot += absRotationDelta;
        CoreRotX -= absRotationDelta;
        Quaternion LegQauternion = Quaternion.AngleAxis(LegRot, Vector3.up);
        transform.localRotation = Quaternion.identity * LegQauternion;



        //play thruster particles
        if (ControlledFrame.Core.thruster.Thrusting)
            ControlledFrame.Core.thruster.ToggleFwdThruster(true);
       /* else
            ControlledFrame.Core.thruster.ToggleFwdThruster(false);*/
    }

    public void Strafe(float fMagntude)
    {
        if (fMagntude == 0.0f)
        {
            ControlledFrame.myAnimator.SetBool("Strafing", false);
            ControlledFrame.Core.thruster.ToggleLeftThruster(false);
            ControlledFrame.Core.thruster.ToggleRightThruster(false);
            return;
        }

        Vector3 transformVector = gameObject.transform.right;
        transformVector.y = 0;
        transformVector.Normalize();
        float LegSpeed = ControlledFrame.Legs.IsDestroyed ? (ControlledFrame.Legs.Speed / 2) : ControlledFrame.Legs.Speed;
        float speedMultiplier = LegSpeed + ControlledFrame.Core.thruster.ThrusterSpeed;
        gameObject.transform.localPosition -= transformVector * Time.fixedDeltaTime * fMagntude * speedMultiplier;
        ControlledFrame.myAnimator.SetBool("Strafing", true);
        //play thruster particles
        if (ControlledFrame.Core.thruster.Thrusting)
        {
            if(fMagntude > 0.0f)
            {
                ControlledFrame.Core.thruster.ToggleLeftThruster(true);
               // ControlledFrame.Core.thruster.ToggleRightThruster(false);
            }
            else
            {
               // ControlledFrame.Core.thruster.ToggleLeftThruster(false);
                ControlledFrame.Core.thruster.ToggleRightThruster(true);
            }
        }
        /*else
        {
            ControlledFrame.Core.thruster.ToggleLeftThruster(false);
            ControlledFrame.Core.thruster.ToggleRightThruster(false);
        }*/
    }

    public void Aim(float fMagnitudeX, float fMagnitudeY)
    {
        CoreRotX += fMagnitudeX;
        CoreRotY += fMagnitudeY;
        CoreRotX = Mathf.Clamp(CoreRotX, -60.0f, 60.0f);
        CoreRotY = Mathf.Clamp(CoreRotY, -60.0f, 60.0f);
        Quaternion xQuaternion = Quaternion.AngleAxis(CoreRotX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(CoreRotY, -Vector3.right);

        ControlledFrame.Core.transform.localRotation = Quaternion.identity * xQuaternion * yQuaternion;
    }

    public void AimHorizontal(float fMagnitude)
    {
        Camera.main.transform.Rotate(0.0f, fMagnitude, 0.0f);
    }

    public void AimVertical(float fMagnitude)
    {
        Camera.main.transform.Rotate(fMagnitude, 0.0f, 0.0f);
    }

    public void RightArmBeginFire()
    {
        Vector3 target = Camera.main.transform.position + (Camera.main.transform.forward * ControlledFrame.RightHandWeapon.FunctionalRange);
        RaycastHit CameraRayInfo = new RaycastHit();

        RogueMechUtils.SetChildLayerRecursively(gameObject, LayerMask.NameToLayer("Ignore Raycast"));
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out CameraRayInfo, ControlledFrame.RightHandWeapon.FunctionalRange, LayerMask.GetMask(new string[] { "Terrain", "Units" })))
        {
            target = CameraRayInfo.point;
        }
        RogueMechUtils.SetChildLayerRecursively(gameObject, LayerMask.NameToLayer("Units"));

        ControlledFrame.RightHandWeapon.OnFireDown(target);
        /*
        RaycastHit WeaponRayInfo = new RaycastHit();
        Vector3 weaponDirection = (target - ControlledFrame.RightHandWeapon.FirePoint.transform.position).normalized;
        if (Physics.Raycast(ControlledFrame.RightHandWeapon.FirePoint.transform.position, weaponDirection, out WeaponRayInfo, ControlledFrame.RightHandWeapon.FunctionalRange, LayerMask.GetMask(new string[] { "Terrain" })))
        {
            target = WeaponRayInfo.point;
        }*/
    }

    public void RightArmEndFire()
    {
        ControlledFrame.RightHandWeapon.OnFireUp(Vector3.zero);
    }

    public void LeftArmBeginFire()
    {

    }

    public void LeftArmEndFire()
    {

    }

    public void BeginThruster()
    {
        ControlledFrame.Core.thruster.OnThrusterDown();
    }

    public void EndThruster()
    {

        ControlledFrame.Core.thruster.OnThrusterUp();
    }

    public void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        if (ControlledFrame.Arms.IsDestroyed)
        {
            float x = 0, y = 0;
            y += verticalSwayAmount * Mathf.Sin((swaySpeed * 2) * Time.time);
            x += horiztonalSwayAmount * Mathf.Sin(swaySpeed * Time.time);
            Aim(x, y);
        }
    }
}
