using System;
using System.Collections.Generic;
using System.Linq;

namespace Cards
{
    public static class CardsExtension
    {
        /// <summary>
        /// Sorts the cards.
        /// </summary>
        /// <param name="cards">The cards</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Collection has duplicates or missing elements</exception>
        /// <exception cref="ArgumentNullException"><paramref name="cards"/> is <see langword="null"/></exception>
        public static IEnumerable<Card> SortCards(this IEnumerable<Card> cards)
        {
            if (cards == null) throw new ArgumentNullException(nameof(cards));
            if (!cards.Any()) return Enumerable.Empty<Card>();

            Dictionary<string, Card> sourceCityToCard;
            Dictionary<string, Card> destinationCityToCard;

            try
            {
                destinationCityToCard = cards.ToDictionary(card => card.DestinationCity);//N*C1
                sourceCityToCard = cards.ToDictionary(card => card.SourceCity);//N*C2
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Collection has duplicates!", nameof(cards));
            }

            Card firstCard = default;

            foreach (var card in cards)//Max(N*C3)
            {
                if (!destinationCityToCard.ContainsKey(card.SourceCity))
                {
                    firstCard = card;
                    break;
                }
            }

            if (firstCard == default)
            {
                throw new ArgumentException(nameof(cards));
            }

            var sortedCards = new Card[destinationCityToCard.Count];

            var currentCard = firstCard;
            try
            {
                for (var i = 0; i < destinationCityToCard.Count; i++)//N*C4
                {
                    sortedCards[i] = currentCard;
                    sourceCityToCard.Remove(currentCard.SourceCity);
                    if (sourceCityToCard.Any())
                    {
                        currentCard = sourceCityToCard[currentCard.DestinationCity];
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Collection missing elements!", nameof(cards));
            }
            //total N*(C1+C2+C3+C4) -> N
            return sortedCards;
        }
    }
}