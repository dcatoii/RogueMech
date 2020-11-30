using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : FrameAccessory {

    public GameObject ProjectilePrefab;
    public GameObject FirePoint;
    public float FunctionalRange = 200.0f;
    public float RefireTime = 0.4f;
    public float TimeSinceLastFire = 0.0f;

    public int Weight = 1000;
    public int EnergyCost = 400;
    public int damage = 0;
    public WeaponHUD HUDPrefab;

    // Use this for initialization
    protected override void Start() {
        base.Start();
    }

    public virtual void OnFireDown(Vector3 target)
    {
        if (TimeSinceLastFire < RefireTime)
            return;

        GameObject newProjectileObject = (GameObject.Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as GameObject);
        newProjectileObject.GetComponent<Projectile>().Source = GetComponentInParent<MechFrame>();
        newProjectileObject.GetComponent<Projectile>().SetTarget(target);
        newProjectileObject.GetComponent<Projectile>().SetDamage(damage);
        TimeSinceLastFire = 0.0f;
    }

    public virtual void OnFireUp(Vector3 target)
    {

    }

    protected virtual void FixedUpdate()
    {
        if (ApplicationContext.Game.IsPaused)
            return;

        TimeSinceLastFire += Time.fixedDeltaTime;
    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Energy Cost");
        returnList.Add("Weight");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();

        returnList.Add(EnergyCost.ToString());
        returnList.Add(Weight.ToString());

        return returnList;
    }

    public Vector3 GetTargetWithCameraRay(Vector3 endPoint)
    {
        Vector3 target = endPoint;
        RaycastHit[] TerrainRayInfo, UnitRayInfo, CameraRayInfo;

        RogueMechUtils.SetChildLayerRecursively(Mech.MechRoot.gameObject, LayerMask.NameToLayer("Ignore Raycast"));

        //The correct way, but we cannot use this due to a unity-level bug for now
        /*CameraRayInfo = Physics.RaycastAll(Camera.main.transform.position,
                                            (target - Camera.main.transform.position).normalized,
                                            Mech.RightHandWeapon.FunctionalRange,
                                            LayerMask.GetMask(new string[] { "Terrain", "Units" }));
                                            */

        /////////////////////////TODO: Write and use my own RaycastAll function//////////////////////

        //////////////////unity raycast bug workaround/////////////////////////
        TerrainRayInfo = Physics.RaycastAll(Camera.main.transform.position,
                                            (target - Camera.main.transform.position).normalized,
                                            FunctionalRange,
                                            LayerMask.GetMask(new string[] { "Terrain" }));

        UnitRayInfo = Physics.RaycastAll(Camera.main.transform.position,
                                            (target - Camera.main.transform.position).normalized,
                                            FunctionalRange,
                                            LayerMask.GetMask(new string[] { "Units" }));

        List<RaycastHit> AllHits = new List<RaycastHit>(TerrainRayInfo);
        AllHits.AddRange(UnitRayInfo);
        CameraRayInfo = AllHits.ToArray();
        ////////////////////////END WORKAROUND/////////////////////

        if (CameraRayInfo.Length > 0)
        {
            CameraRayInfo = RogueMechUtils.SortRaycastArrayByDistance(CameraRayInfo);
            foreach (RaycastHit hitInfo in CameraRayInfo)
            {
                //make sure the weapon can actually aim at the target
                if (Vector3.Dot(transform.forward, (hitInfo.point - FirePoint.transform.position)) > 0)
                {
                    target = hitInfo.point;
                    break;
                }
            }
        }
        RogueMechUtils.SetChildLayerRecursively(Mech.MechRoot.gameObject, LayerMask.NameToLayer("Units"));

        return target;
    }
}
