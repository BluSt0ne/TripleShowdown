using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;

    // To keep track of original position when drag starts
    private Vector3 originalPosition;

    // Track the current state of the dice (whether it's being dragged or not)
    private bool isDragging = false;

    // Use this for initialization
    private void Start()
    {
        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites from the DiceSides folder
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    // New public method that can be called by the UI button
    public void RollDice()
    {
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;

        // Loop to switch dice sides randomly
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (inclusive)
            randomDiceSide = Random.Range(0, diceSides.Length);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next iteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        Debug.Log(finalSide);
    }

    // Called when the mouse is clicked on the dice
    public void OnPointerDown(PointerEventData eventData)
    {
        // Save the original position when the drag starts
        originalPosition = transform.position;

        // Start the drag
        isDragging = true;
    }

    // Called when the mouse is dragging the dice
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Move the dice to the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
            mousePosition.z = 0; // Keep the z-axis fixed (if needed for 2D)
            transform.position = mousePosition;
        }
    }

    // Called when the mouse button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        // Stop dragging and snap back to original position (optional)
        isDragging = false;

        // You can keep it at the new position or snap back to the original position:
        // transform.position = originalPosition; // Uncomment this line if you want to snap back
    }
}
