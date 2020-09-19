using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public FrameController ArmorFrame;
    public float Sensitivity = 6.0f;

    bool isWeapon1InUse = false;
    bool isThrusterInUse = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void FixedUpdate () {

        HandleThruster();

        ArmorFrame.MoveForward(Input.GetAxis("Forward"));
        ArmorFrame.Strafe(Input.GetAxis("Strafe"));

        ArmorFrame.Aim(Input.GetAxis("Mouse X") * Sensitivity, Input.GetAxis("Mouse Y")*Sensitivity);

        HandleWeapon1Input();

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
}
