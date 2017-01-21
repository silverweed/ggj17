using UnityEngine;

/// <summary>
/// Inertial chasing object on the X axis. With lazors.
/// </summary>
public class FollowObject : MonoBehaviour {

    public GameObject target;

    const float mass = 1f;
    const float tension = 1f;
    const float drag = 20f;

    float speed = 0f;

    void Update() {
        speed = ApplyTension(speed, target.transform.position.x - transform.position.x, Time.deltaTime);
        speed = ApplyFriction(speed, Time.deltaTime);
        transform.position = transform.position + new Vector3(speed, 0f);
    }

    float ApplyTension(float speed, float distance, float deltaTime) {
        var force = distance * tension;
        var acceleration = force / mass;
        return speed + (acceleration * deltaTime);
    }

    float ApplyFriction(float speed, float deltaTime) {
        var force = -speed * drag;
        var acceleration = force / mass;
        return speed + (acceleration * deltaTime);
    }
}
