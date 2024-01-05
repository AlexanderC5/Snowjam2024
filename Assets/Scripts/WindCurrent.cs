using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindCurrent : MonoBehaviour
{
    public enum CurrentType
    {
        Directional,
        Radial 
    };

    [SerializeField] public bool RandomizeSize = false;
    [SerializeField] public bool RandomizeStrength = false;
    [SerializeField] public Vector2 Direction;
    [SerializeField] public float Strength = 1.0f;
    [SerializeField] public CurrentType Type;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<ParticleSystem>(out particles))
            Debug.LogWarning("No Particle System");
        if (Direction.magnitude == 0)
            Direction = Random.insideUnitCircle;
        Direction = Direction.normalized;
        if (RandomizeSize)
            transform.localScale *= 0.5f + Random.value;
        if (RandomizeStrength)
            Strength *= 1 - 2*Random.value;

        // configure particle system depending on wind current
        var velOverLifetime = particles.velocityOverLifetime;
        switch (Type)
        {
            case (CurrentType.Radial):
                velOverLifetime.radial = Strength / 2;
                break;
            case (CurrentType.Directional):
                velOverLifetime.radial = 0;
                velOverLifetime.x = Direction.x * Strength / 2;
                velOverLifetime.y = Direction.y * Strength / 2;
                break;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.attachedRigidbody != null)
        {
            Vector2 force = FindForce(collider.transform);
            collider.attachedRigidbody.AddForce(force);
        }
    }
    
    private Vector2 FindForce(Transform otherTransform)
    {
        Vector2 displacement = otherTransform.position - transform.position;
        Vector2 force;
        switch(Type)
        {
            case CurrentType.Radial:
                force = displacement * Strength;
                break;
            case CurrentType.Directional:
                force = Direction * Strength;
                break;
            default:
                force = new Vector2(0, 0);
                break;
        }
        return force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
