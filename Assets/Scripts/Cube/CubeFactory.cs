using UnityEngine;
using Lean.Common;

public class CubeFactory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CubeController _cubePrefab;
    [SerializeField] private Transform _spawnPoint;

    [Header("Settings")]
    [SerializeField] private float _probabilityForTwo = 0.75f; // 75%

    public CubeController CreateCube()
    {
        CubeController newCube = Lean.Pool.LeanPool.Spawn(_cubePrefab, _spawnPoint.position, Quaternion.identity);
        int value = GetRandomValue();

        return newCube;
    }

    public int GenerateValue() => GetRandomValue();

    private int GetRandomValue()
    {
        return Random.value <= _probabilityForTwo ? 2 : 4;
    }
}