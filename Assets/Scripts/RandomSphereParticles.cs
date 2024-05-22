using UnityEngine;

public class RandomSphereParticles : MonoBehaviour
{
    public int numberOfParticles = 1000;
    public float sphereRadius = 100f;
    public Gradient colorGradient;
    public float minParticleSize = 0.1f;
    public float maxParticleSize = 2.0f;
    public float minLifetime = 1.0f;
    public float maxLifetime = 5.0f;

    private void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numberOfParticles];

        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * sphereRadius;
            float normalizedAge = Random.Range(0.0f, 1.0f);

            particles[i].position = randomPosition;
            particles[i].startSize = Random.Range(minParticleSize, maxParticleSize);
            particles[i].startLifetime = Random.Range(minLifetime, maxLifetime);
            particles[i].startColor = colorGradient.Evaluate(normalizedAge);
        }

        particleSystem.SetParticles(particles, particles.Length);
    }
}
