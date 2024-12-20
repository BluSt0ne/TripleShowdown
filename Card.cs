using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TripleShowdown
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card: ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public int multiply;
        public List<DamageType> damageType;
        public Sprite cardSprite;

        public GameObject prefab;

        public enum CardType
        {
            Attack,
            Guard,
            Multiply
        }
            public enum DamageType
        {
            Attack,
            Guard,
            Multiply
        }
    }
}

