using UnityEngine;

public class DiceUI : MonoBehaviour
{
    private Animator animator;
    private int diceResult;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RollDice()
    {
        // Start dice rolling animation
        animator.SetTrigger("Roll");

        // Randomize dice result
        diceResult = Random.Range(1, 7);

        // Stop the animation after a short delay and show the result
        Invoke("ShowDiceResult", 1.5f);
    }

    private void ShowDiceResult()
    {
        Debug.Log("Dice Result: " + diceResult);
        // Optional: Update the dice's sprite to show the final result
    }
}
