using UnityEngine;

public class BaseTurret : IWeapon
{
    private int baseDamage = 10;

    public int GetDamage()
    {
        return baseDamage;
    }

    public void Fire()
    {
        Debug.Log("Turret Firing. Damage: " + GetDamage());
    }
}
