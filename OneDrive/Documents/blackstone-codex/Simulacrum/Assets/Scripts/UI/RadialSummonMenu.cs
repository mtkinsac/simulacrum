using UnityEngine;

public class RadialSummonMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public float activationDelay = 0.5f;
    private bool isMenuActive = false;

    void Update()
    {
        // Replace with XR gesture-based input in production.
        if (Input.GetKeyDown(KeyCode.R))
            ToggleMenu();
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);
        Debug.Log("RadialSummonMenu: " + (isMenuActive ? "Activated." : "Deactivated."));
    }

    public void SummonBook()
    {
        Debug.Log("RadialSummonMenu: Summoning a book.");
        // TODO: Integrate with LibraryShelfSpawner or instantiate a book prefab.
    }

    public void SummonPDF()
    {
        Debug.Log("RadialSummonMenu: Summoning a PDF.");
        // TODO: Integrate with PDFLoader to display a PDF.
    }
}
