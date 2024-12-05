using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CornerCubeSettings
{
    public GameObject cornerCube;
    public float speed = 5f;
    [Header("Birikim oluşması için")]

    public float offset = 0f;
}

public class SpawnCube : MonoBehaviour
{
    public static SpawnCube Instance;

    [Header("Cube Settings")]
    public GameObject cubePrefab;
    public Transform spawnPosition;
    public int cubeCount = 30;
    public float spawnInterval = 1f;
    public float productSpeed = 5f;

    [Header("Corner Cube Settings")]
    public List<CornerCubeSettings> cornerCubes = new List<CornerCubeSettings>();

    [Header("Branch System")]
    public GameObject branch1;
    public GameObject branch2;
    public float switchInterval = 1f;

    private float timer;
    private bool isBranch1Active = true; // Başlangıçta branch1 aktif

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            if (isBranch1Active)
            {
                ToggleCollider(branch1, false); // branch1'i kapat
                ToggleCollider(branch2, true);  // branch2'yi aç
            }
            else
            {
                ToggleCollider(branch1, true);  // branch1'i aç
                ToggleCollider(branch2, false); // branch2'yi kapat
            }

            isBranch1Active = !isBranch1Active; // Durumu değiştir
            timer = 0f;
        }
    }

    IEnumerator SpawnCubes()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            Instantiate(cubePrefab, spawnPosition.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public (float speed, float offset) GetCornerCubeProperties(GameObject cube)
    {
        foreach (var corner in cornerCubes)
        {
            if (corner.cornerCube == cube)
            {
                return (corner.speed, corner.offset);
            }
        }
        return (productSpeed, 0);
    }

    private void ToggleCollider(GameObject obj, bool state)
    {
        if (obj.TryGetComponent(out Collider collider))
        {
            collider.enabled = state;
        }
        else
        {
            Debug.LogWarning($"{obj.name} nesnesinde Collider yok.");
        }
    }
}
