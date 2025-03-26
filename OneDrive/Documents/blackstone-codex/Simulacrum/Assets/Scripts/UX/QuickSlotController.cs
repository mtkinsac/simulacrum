using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    public void SummonItem(int slotIndex)
    {
        Debug.Log("QuickSlotController: Summoning item from slot: " + slotIndex);
        // TODO: Trigger summoning logic for the selected slot.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SummonItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SummonItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SummonItem(3);
    }
}
