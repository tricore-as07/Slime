using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGimmik : MonoBehaviour
{
    [SerializeField] float WindPower = 0f;
    Rigidbody playerRigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == TagName.Player)
        {
            playerRigidbody = playerRigidbody ?? collision.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.up * WindPower);
        }
    }
}
