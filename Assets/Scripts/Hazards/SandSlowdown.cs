using UnityEngine;

public class SandSlowdown : MonoBehaviour
{
    [SerializeField] private int slownessValue;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().linearDamping = slownessValue;
            
        }
    }

}
