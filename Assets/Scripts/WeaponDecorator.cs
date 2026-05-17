public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrappedWeapon;

    public WeaponDecorator(IWeapon weapon)
    {
        this.wrappedWeapon = weapon;
    }

    public virtual int GetDamage()
    {
        return wrappedWeapon.GetDamage();
    }

    public virtual void Fire()
    {
        wrappedWeapon.Fire();
    }
}