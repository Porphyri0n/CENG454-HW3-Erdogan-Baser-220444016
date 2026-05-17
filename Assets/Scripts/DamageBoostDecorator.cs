using UnityEngine;

public class DamageBoostDecorator : WeaponDecorator
{
    private int bonusDamage = 15;

    public DamageBoostDecorator(IWeapon weapon) : base(weapon)
    {
    }

    public override int GetDamage()
    {
        return base.GetDamage() + bonusDamage;
    }

    public override void Fire()
    {
        base.Fire();
        Debug.Log("Bonus Hasar Aktif!");
    }
}