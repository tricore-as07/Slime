using UnityEngine;

/// <summary>
/// モーターの力を加える処理をする
/// </summary>
public class AddMotorForce : MonoBehaviour
{
    [SerializeField] HingeJoint joint = default;
    [SerializeField] float force = 0f;
    [SerializeField] float targetVelocity = 0f;
    [SerializeField] float deadZoneVelocity = 0f;

    // Update is called once per frame
    void Update()
    {
        if(joint.autoConfigureConnectedAnchor)
        {
            var motor = joint.motor;
            if (joint.connectedBody.velocity.x > deadZoneVelocity)
            {
                motor.force = force;
                motor.targetVelocity = -targetVelocity;
            }
            else if (joint.connectedBody.velocity.x < -deadZoneVelocity)
            {
                motor.force = force;
                motor.targetVelocity = targetVelocity;
            }
            else
            {
                motor.force = 0f;
                motor.targetVelocity = 0f;
            }
            joint.motor = motor;
        }
    }
}
