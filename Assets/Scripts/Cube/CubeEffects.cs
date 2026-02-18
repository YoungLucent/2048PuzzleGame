using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CubeEffects : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _explosionForce = 600f;
    [SerializeField] private float _explosionRadius = 2.5f;

    private readonly Collider[] _explosionResults = new Collider[10];

    public void PlayMergeEffect()
    {
        PlayPopEffect();
        ExplodeNearby();
    }

    private void ExplodeNearby()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _explosionResults);

        for (int i = 0; i < count; i++)
        {
            var col = _explosionResults[i];
            if (col == null) continue;

            if (col.attachedRigidbody != null && !col.attachedRigidbody.isKinematic)
            {
                col.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    public void PlayPopEffect()
    {
        transform.DOKill(true);
        transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 1, 0.5f);
    }
}