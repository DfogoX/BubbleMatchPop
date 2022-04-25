using UnityEngine;

public abstract class InputSystem : MonoBehaviour
{
    private Vector3 interaction;
    private LayerMask platformLayer;

    protected abstract bool IsInteracting();
    protected abstract Vector3 GetInteraction();
    
    public void UpdateInput()
    {
        if (!IsInteracting()) return;
        
        interaction = GetInteraction();
        var ray = Camera.main.ScreenPointToRay(interaction);

        RaycastHit hitData;
        //check if a platform was hit
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red, 2.0f );
        if (Physics.Raycast(ray, out hitData, Mathf.Infinity, platformLayer))
        {
            Debug.Log("was hit");
            var plat = hitData.collider.GetComponent<Platform>();
            // could validate if the collider exist, to be extra sure, even though the prefab contains it
            if (plat != null)
            {
                plat.RotatePlatform();
            }
            else
            {
                Debug.LogWarning($"Platform Script not found after hit");
            }
        }
    }

    public void SetLayer(LayerMask platformLayer)
    {
        this.platformLayer = platformLayer;
    }
}