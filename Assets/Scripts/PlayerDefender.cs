using UnityEngine;

public class PlayerDefender : MonoBehaviour
{
    private IWeapon currentWeapon;

    private void Start()
    {
        currentWeapon = new BaseTurret();
        currentWeapon.Fire();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyPowerUp();
        }

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Fire();
            ShootRaycast();
        }
    }

    private void ShootRaycast()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(currentWeapon.GetDamage());
            }
        }
    }

    private void ApplyPowerUp()
    {
        currentWeapon = new DamageBoostDecorator(currentWeapon);
        Debug.Log("System Updated: Damage Boost Applied!");
    }
}
