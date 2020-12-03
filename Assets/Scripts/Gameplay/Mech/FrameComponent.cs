using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameComponent : FramePart
{

    public int MaxArmor;
    public int ArmorPoints;
    public bool IsDestroyed {  get { return ArmorPoints <= 0; } }

    protected bool isInvulnerable = false;
    public bool IsInvulnerable { get { return isInvulnerable; } }
    
    public virtual void OnHit(Projectile projectile)
    {
        isInvulnerable = true;
        if (Mech == null)
            Start(); // if we get hit before initialization, force init now
    }

    protected virtual void FixedUpdate()
    {
        //by default part invulnerability wears off next frame
        isInvulnerable = false;
    }

    public virtual void TakeDamage(int amount)
    {
        if(!IsDestroyed)
        {
            ArmorPoints = Mathf.Clamp(ArmorPoints - amount, 0, MaxArmor);
            if (IsDestroyed)
                OnPartBroken();
        }
    }

    protected virtual void OnPartBroken()
    {

    }

    public override List<string> GetAttributeNamesForStore()
    {
        List<string> returnList = base.GetAttributeNamesForStore();
        returnList.Add("Armor Points");
        return returnList;
    }

    public override List<string> GetAttributeValuesForStore()
    {
        List<string> returnList = base.GetAttributeValuesForStore();
        returnList.Add(MaxArmor.ToString());
        return returnList;
    }
}
