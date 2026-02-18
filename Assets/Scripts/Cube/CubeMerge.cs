using UnityEngine;
using Lean.Pool;

public class CubeMerge : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _mergeImpulseThreshold = 1.5f;

    private CubeController _controller;

    public void Initialize(CubeController controller)
    {
        _controller = controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_controller.IsMerging) return;
        if (!collision.gameObject.TryGetComponent(out CubeController otherCube)) return;

        if (CanMerge(otherCube, collision))
        {
            ExecuteMerge(otherCube);
        }
    }

    private bool CanMerge(CubeController otherCube, Collision collision)
    {
        if (!otherCube.gameObject.activeInHierarchy) return false;
        if (otherCube.IsMerging) return false;
        if (_controller.Value != otherCube.Value) return false;

        if (GetInstanceID() < otherCube.GetInstanceID()) return false;

        if (collision.relativeVelocity.magnitude < _mergeImpulseThreshold) return false;

        return true;
    }

    private void ExecuteMerge(CubeController otherCube)
    {
        _controller.StartMergeProcess();
        otherCube.StartMergeProcess();

        otherCube.gameObject.SetActive(false);
        LeanPool.Despawn(otherCube.gameObject);

        _controller.ApplyMergeSuccess();
    }
}