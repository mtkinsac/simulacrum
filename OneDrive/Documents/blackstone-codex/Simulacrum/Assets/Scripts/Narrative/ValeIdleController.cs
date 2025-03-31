using UnityEngine;
using System.Collections;

public class ValeIdleController : MonoBehaviour
{
    public float idleThreshold = 30f;
    public float promptInterval = 60f;
    public TextAsset[] idlePromptFiles;

    private float idleTimer = 0f;
    private float lastPromptTime = -100f;
    private ValeDialogueManager dialogueManager;

    void Awake()
    {
        dialogueManager = FindObjectOfType<ValeDialogueManager>();
    }

    void Update()
    {
        if (Input.anyKey)
            idleTimer = 0f;
        else
            idleTimer += Time.deltaTime;

        if (idleTimer >= idleThreshold && (Time.time - lastPromptTime) >= promptInterval)
        {
            TriggerIdlePrompt();
            lastPromptTime = Time.time;
            idleTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TriggerIdlePrompt();
            lastPromptTime = Time.time;
            idleTimer = 0f;
        }
    }

    void TriggerIdlePrompt()
    {
        if (idlePromptFiles == null || idlePromptFiles.Length == 0) return;
        int index = Random.Range(0, idlePromptFiles.Length);
        string prompt = idlePromptFiles[index].text;

        if (dialogueManager != null)
        {
            dialogueManager.HandleValeSpeakRequest(prompt);
            Debug.Log("ValeIdleController: Triggered idle prompt: " + prompt);
        }
    }
}
