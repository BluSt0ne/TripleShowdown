using System.Collections;
using System.Collections.Generic;
using TripleShowdown;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public int startingHandSize = 5;
    private int currentIndex = 0;
    public int maxHandSize = 10;  // Set a default max hand size
    public int currentHandSize;
    private HandManager handManager;

    void Start()
    {
        // Load all card assets from the resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");
        // Add the loaded cards to the allCards list
        allCards.AddRange(cards);

        // Shuffle the deck (Fisher-Yates shuffle)
        ShuffleDeck();

        // Find and initialize handManager
        handManager = FindObjectOfType<HandManager>();
        if (handManager == null)
        {
            Debug.LogError("HandManager not found in the scene.");
            return;
        }

        // Draw starting hand
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard(handManager);
        }
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            return;
        }
        if (currentHandSize < maxHandSize)
        {
            // Randomly shuffle the deck each time a card is drawn
            ShuffleDeck();

            // Draw a random card from the shuffled deck
            Card nextCard = allCards[currentIndex];
            handManager.AddCardToHand(nextCard);

            // Update currentIndex to pick the next card in the shuffled list
            currentIndex = (currentIndex + 1) % allCards.Count;
        }
    }

    // Shuffle the deck using Fisher-Yates algorithm
    public void ShuffleDeck()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            // Get a random index in the remaining part of the deck
            int j = Random.Range(i, allCards.Count);
            // Swap the current element with the randomly selected one
            Card temp = allCards[i];
            allCards[i] = allCards[j];
            allCards[j] = temp;
        }
    }
}
