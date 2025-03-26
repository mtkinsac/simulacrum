using UnityEngine;

public class MemoryObject : MonoBehaviour
{
    [Header("Memory Object Settings")]
    public MemoryEchoData memoryData;

    private LoreInteractionManager loreInteractionManager;
    public Renderer objectRenderer;
    public Color highlightColor = Color.yellow;
    private Color originalColor;

    void Start()
    {
        loreInteractionManager = FindObjectOfType<LoreInteractionManager>();
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
            originalColor = objectRenderer.material.color;
    }

    public void OnInteract()
    {
        if (loreInteractionManager != null && memoryData != null)
            loreInteractionManager.TriggerMemory(memoryData.id);
        else
            Debug.LogWarning("MemoryObject: LoreInteractionManager or memoryData not assigned.");
    }

    public void OnHoverEnter()
    {
        if (objectRenderer != null)
            objectRenderer.material.color = highlightColor;
    }

    public void OnHoverExit()
    {
        if (objectRenderer != null)
            objectRenderer.material.color = originalColor;
    }
}
