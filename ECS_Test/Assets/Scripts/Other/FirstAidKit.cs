using UnityEngine;

public class FirstAidKit : MonoBehaviour, ITrigger
{
    public int Heal;

    public void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ChangeHealth(Heal);
            gameObject.SetActive(false);
        }
    }
}
