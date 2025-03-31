using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ValeReflectionManager : MonoBehaviour
{
    public ValeDialogueManager valeDialogueManager;

    // Queue for reflection lines
    private Queue<string> reflectionQueue = new Queue<string>();
    private bool isProcessingQueue = false;

    void Awake()
    {
        if (valeDialogueManager == null)
        {
            valeDialogueManager = FindObjectOfType<ValeDialogueManager>();
        }
    }

    /// <summary>
    /// Enqueues a reflection line from a memory object.
    /// </summary>
    public void TriggerMemoryReflection(MemoryObjectData memoryData)
    {
        if (memoryData == null || string.IsNullOrEmpty(memoryData.reflectionLine))
            return;

        reflectionQueue.Enqueue(memoryData.reflectionLine);
        if (!isProcessingQueue)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isProcessingQueue = true;
        while (reflectionQueue.Count > 0)
        {
            string nextLine = reflectionQueue.Dequeue();
            if (valeDialogueManager != null)
            {
                valeDialogueManager.HandleValeSpeakRequest(nextLine);
            }
            else
            {
                Debug.LogWarning("ValeReflectionManager: ValeDialogueManager is not assigned!");
            }
            yield return new WaitForSeconds(5f); // Delay between reflections
        }
        isProcessingQueue = false;
    }
}
