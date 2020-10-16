using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public FrameController ArmorFrame;

    bool isWeapon1InUse = false;
    bool isThrusterInUse = false;
    bool isInViewTransition = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (ApplicationContext.Game.IsPaused)
            return;

        HandleThruster();

        ArmorFrame.MoveForward(Input.GetAxis("Forward"));
        ArmorFrame.Strafe(Input.GetAxis("Strafe"));
        
        HandleWeapon1Input();

        HandleViewTransition();
    }

    private void Update()
    {
        //trying to move this to update to see if we get smoother aiming
        ArmorFrame.Aim(Input.GetAxis("Mouse X") * PlayerData.Sensitivity, Input.GetAxis("Mouse Y") * PlayerData.Sensitivity);
    }

    void HandleWeapon1Input()
    {
        if (!isWeapon1InUse)
        {
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                ArmorFrame.RightArmBeginFire();
                isWeapon1InUse = true;
            }
        }
        else if (Input.GetAxisRaw("Fire1") == 0)
        {
            ArmorFrame.RightArmEndFire();
            isWeapon1InUse = false;
        }
    }

    void HandleThruster()
    {
        if (!isThrusterInUse)
        {
            if (Input.GetAxisRaw("Thruster") > 0)
            {
                ArmorFrame.BeginThruster();
                isThrusterInUse = true;
            }
        }
        else if (Input.GetAxisRaw("Thruster") == 0)
        {
            ArmorFrame.EndThruster();
            isThrusterInUse = false;
        }
    }

    void HandleViewTransition()
    {
        if (isInViewTransition)
            return;

        if (Input.GetAxisRaw("ChangeView") > 0)
        {
            if(Camera.main.transform.parent == ArmorFrame.ControlledFrame.Core.CameraAnchor.transform)
            {
                Camera.main.transform.parent = ArmorFrame.ControlledFrame.Core.HeadSocket.transform;
            }
            else
            {
                Camera.main.transform.parent = ArmorFrame.ControlledFrame.Core.CameraAnchor.transform;
            }
            LTDescr desc = LeanTween.moveLocal(Camera.main.gameObject, Vector3.zero, 0.33f);
            desc.setOnComplete(TransitionComplete);
            isInViewTransition = true;
        }

    }

    void TransitionComplete()
    {
        isInViewTransition = false;
    }
}
