using UnityEngine;
using UnityEngine.UI;

public class MotherDroneCommunication : MonoBehaviour
{
    [SerializeField] private Text survivorCountText;

    // Function to be called by the soldier drone to send the survivor count
    public void ReceiveSurvivorCount(int count)
    {
        // Update the UI text to display the number of survivors spotted
        survivorCountText.text = "Survivors Spotted: " + count;
    }
}