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
        }
    }

    private void ApplyPowerUp()
    {
        currentWeapon = new DamageBoostDecorator(currentWeapon);
        Debug.Log("Sistem GÃ¼ncellendi: Hasar GÃ¼Ã§lendirici TakÄ±ldÄ±!");
    }
}
