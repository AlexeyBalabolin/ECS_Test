using System.Collections;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject BulletPrefab;
    public float rechargeTime;
    bool _canShoot = true;

    public void Execute()
    {
        if(_canShoot)
        {
            Instantiate(BulletPrefab?.gameObject, transform.position, transform.rotation);
            _canShoot = false;
            StartCoroutine(Recharge(rechargeTime));
        }
    }

    IEnumerator Recharge(float recharge)
    {
        yield return new WaitForSeconds(recharge);
        _canShoot = true;
    }
}
