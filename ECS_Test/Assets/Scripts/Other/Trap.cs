using UnityEngine;

public class Trap : MonoBehaviour, ITrigger
{
    public int Damage;

    public void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ChangeHealth(-Damage);
            gameObject.SetActive(false);
        }
    }
}
