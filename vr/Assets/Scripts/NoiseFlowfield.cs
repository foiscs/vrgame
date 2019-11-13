using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFlowfield : MonoBehaviour
{
    FastNoise fastNoise;
    public Vector3Int gridSize;
    public Vector3 offset, offsetSpeed;
    public Vector3[,,] flowfieldDirection;
    public float increment;
    public float cellSize;

    //particles
    public GameObject particlePrefeb;
    public int amountOfParticles;
    [HideInInspector]
    public List<flowfieldParticle> particles;
    public float spawnRadius;
    public float particleScale , particleMoveSpeed, particleRotationSpeed;
    bool particleSpawnValidation(Vector3 position)
    {
        bool valid = true;
        foreach (flowfieldParticle item in particles)
        {
            if(Vector3.Distance(position, item.transform.position) < spawnRadius)
            {
                valid = false;
                break;
            }
        }
        if(valid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Start()
    {
        flowfieldDirection = new Vector3[gridSize.x, gridSize.y, gridSize.z];
        fastNoise = new FastNoise();
        particles = new List<flowfieldParticle>();
        for (int i = 0; i < amountOfParticles; i++)
        {
            int attempt = 0;
            while (attempt < 100)
            {


                Vector3 randomPos = new Vector3(
                    Random.Range(this.transform.position.x, this.transform.position.x + gridSize.x * cellSize),
                    Random.Range(this.transform.position.y, this.transform.position.y + gridSize.y * cellSize),
                    Random.Range(this.transform.position.z, this.transform.position.z + gridSize.z * cellSize));
                bool isValid = particleSpawnValidation(randomPos);

                if (isValid)
                {
                    GameObject particleInstance = (GameObject)Instantiate(particlePrefeb);
                    particleInstance.transform.position = randomPos;
                    particleInstance.transform.parent = this.transform;
                    particleInstance.transform.localScale = new Vector3(particleScale, particleScale, particleScale);

                    particles.Add(particleInstance.GetComponent<flowfieldParticle>());
                    break;
                }
                if(!isValid)
                {
                    attempt++;
                }
            }
        }
        Debug.Log(particles.Count);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateFlowfieldDorectons();
        ParticleBehaviour();
    }
    void CalculateFlowfieldDorectons()
    {
        offset = new Vector3(offset.x + (offsetSpeed.x * Time.deltaTime), offset.y + (offsetSpeed.y * Time.deltaTime), offset.z + (offsetSpeed.z * Time.deltaTime));

        float xOff = 0;
        for (int x = 0; x < gridSize.x; x++)
        {
            float yOff = 0f;
            for (int y = 0; y < gridSize.y; y++)
            {
                float zOff = 0;
                for (int z = 0; z < gridSize.z; z++)
                {
                    float noise = fastNoise.GetSimplex(xOff + offset.x, yOff + offset.y, zOff + offset.z) + 1;
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                    flowfieldDirection[x, y, z] = Vector3.Normalize(noiseDirection);
                    zOff += increment;
                }
                yOff += increment;
            }
            xOff += increment;
        }
    }
    void ParticleBehaviour()
    {
        foreach (flowfieldParticle item in particles)
        {
            //x
            if(item.transform.position.x > transform.position.x + (gridSize.x*cellSize))
            {
                item.transform.position = new Vector3(this.transform.position.x, item.transform.position.y, item.transform.position.z);
            }
            if(item.transform.position.x < transform.position.x)
            {
                item.transform.position = new Vector3(transform.position.x + (gridSize.x * cellSize), item.transform.position.y, item.transform.position.z);
            }
            //y
            if (item.transform.position.y > transform.position.y + (gridSize.y * cellSize))
            {
                item.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
            }
            if (item.transform.position.y < transform.position.y)
            {
                item.transform.position = new Vector3(item.transform.position.x, transform.position.y + (gridSize.y * cellSize), item.transform.position.z);
            }
            //z
            if (item.transform.position.z > transform.position.z + (gridSize.z * cellSize))
            {
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, this.transform.position.z);
            }
            if (item.transform.position.z < transform.position.z)
            {
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, transform.position.z + (gridSize.z * cellSize));
            }

            Vector3Int particlePos = new Vector3Int(
                Mathf.FloorToInt(Mathf.Clamp((item.transform.position.x - transform.position.x) / cellSize, 0, gridSize.x - 1)),
                Mathf.FloorToInt(Mathf.Clamp((item.transform.position.y - transform.position.y) / cellSize, 0, gridSize.y - 1)),
                Mathf.FloorToInt(Mathf.Clamp((item.transform.position.z - transform.position.z) / cellSize, 0, gridSize.z - 1))
                );
            item.ApplyRotation(flowfieldDirection[particlePos.x,particlePos.y,particlePos.z],particleRotationSpeed);
            item.moveSpeed = particleMoveSpeed;
            item.transform.localScale = new Vector3(particleScale, particleScale, particleScale);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + new Vector3(gridSize.x * cellSize * 0.5f, gridSize.y * cellSize * 0.5f, gridSize.z * cellSize * 0.5f),
            new Vector3(gridSize.x * cellSize, gridSize.y * cellSize, gridSize.z * cellSize));
    }
}
