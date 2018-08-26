using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cards.UnitTests
{
    public class CardsExtensionTests
    {
        private readonly IReadOnlyList<Card> _testCardsEnumerable;

        public CardsExtensionTests()
        {
            _testCardsEnumerable = Enumerable.Range(1, 100).Select(i => new Card((i - 1).ToString(), i.ToString())).ToList();
        }

        [Fact]
        public void SortCards_CardEnumerableHasDublicates_ArgumentException()
        {
            var cards = _testCardsEnumerable.ToList();
            cards.AddRange(_testCardsEnumerable);
            Assert.Throws<ArgumentException>(() => cards.SortCards());
        }

        [Fact]
        public void SortCards_CardEnumerableMissingElement_ArgumentException()
        {
            var cards = _testCardsEnumerable.ToList();
            cards.RemoveAt(cards.Count - 2);
            Assert.Throws<ArgumentException>(() => cards.SortCards());
        }

        [Fact]
        public void SortCards_EmptyCardEnumerable_EmptyResult()
        {
            Assert.Empty(Enumerable.Empty<Card>().SortCards());
        }

        [Fact]
        public void SortCards_ReverseCardEnumerable_TestCardEnumerableResult()
        {
            Assert.Equal(_testCardsEnumerable.Reverse().SortCards(), _testCardsEnumerable);
        }

        [Fact]
        public void SortCards_ShuffledCardEnumarable_TestCardEnumerableResult()
        {
            var cards = _testCardsEnumerable.ToList();

            for (var i = 0; i < cards.Count - 2; i += 2)
            {
                var temp = cards[i];
                cards[i] = cards[i + 1];
                cards[i + 1] = temp;
            }

            Assert.Equal(cards.SortCards(), _testCardsEnumerable);
        }

        [Fact]
        public void SortCards_SingleCardEnumerable_SingleCardResult()
        {
            var card = new Card("1", "2");
            var result = new[] { card }.SortCards();

            Assert.Single(result);
            Assert.Same(card, result.First());
        }

        [Fact]
        public void SortCards_TestCardEnumerable_TestCardEnumerableResult()
        {
            Assert.Equal(_testCardsEnumerable.SortCards(), _testCardsEnumerable);
        }
    }
}