using UnityEngine;

public class GazePromptTrigger : MonoBehaviour
{
    public float gazeThreshold = 2.0f; // Seconds to trigger a prompt.
    private float gazeTimer = 0f;
    private GameObject currentGazeObject = null;
    
    public float cooldownTime = 5.0f; // Cooldown period.
    private float cooldownTimer = 0f;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }
        
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("GazeInteractive"))
            {
                if (currentGazeObject == hitObject)
                {
                    gazeTimer += Time.deltaTime;
                    if (gazeTimer >= gazeThreshold)
                    {
                        Debug.Log("GazePromptTrigger: Gaze trigger activated for " + hitObject.name);
                        EventManager.ValeSpeakRequest("GazeTrigger: " + hitObject.name);
                        cooldownTimer = cooldownTime;
                        gazeTimer = 0f;
                    }
                }
                else
                {
                    currentGazeObject = hitObject;
                    gazeTimer = 0f;
                }
            }
            else
            {
                currentGazeObject = null;
                gazeTimer = 0f;
            }
        }
        else
        {
            currentGazeObject = null;
            gazeTimer = 0f;
        }
    }
}
