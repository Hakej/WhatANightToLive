using UnityEngine;

public class HighlightableObject : MonoBehaviour
{
    public Light[] LightsToToggle;
    
    private Renderer[] _renderer;

    private void Start()
    {
        _renderer = GetComponentsInChildren<Renderer>();
    }

    private void OnMouseEnter()
    {
        foreach (var renderer in _renderer)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }

        foreach (var light in LightsToToggle)
        {
            light.intensity = 0.5f;
        }
    }

    private void OnMouseExit()
    {        
        foreach (var renderer in _renderer)
        {
            renderer.material.DisableKeyword("_EMISSION");
        }
        
        
        foreach (var light in LightsToToggle)
        {
            light.intensity = 0f;
        }
    }
}
