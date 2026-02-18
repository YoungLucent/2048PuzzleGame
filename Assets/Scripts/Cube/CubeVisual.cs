using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CubeVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshRenderer _renderer;

    [Header("Config")]
    [SerializeField] private CubeDataConfig _config;

    private static readonly int BaseMapId = Shader.PropertyToID("_BaseMap");

    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    public void UpdateVisuals(int number)
    {
        if (_config == null) return;

        CubeStyle style = _config.GetStyle(number);

        _renderer.GetPropertyBlock(_propBlock);

        if (style.Texture != null)
        {
            _propBlock.SetTexture(BaseMapId, style.Texture);
        }

        _renderer.SetPropertyBlock(_propBlock);
    }

    private void OnValidate()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
    }
}