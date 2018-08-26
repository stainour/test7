using System;

namespace Cards
{
    public class Card : IEquatable<Card>
    {
        public Card(string sourceCity, string destinationCity)
        {
            DestinationCity = destinationCity ?? throw new ArgumentNullException(nameof(destinationCity));
            SourceCity = sourceCity ?? throw new ArgumentNullException(nameof(sourceCity));
        }

        private Card()
        {
        }

        public string DestinationCity { get; }
        public string SourceCity { get; }

        public bool Equals(Card other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DestinationCity, other.DestinationCity) && string.Equals(SourceCity, other.SourceCity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Card)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DestinationCity != null ? DestinationCity.GetHashCode() : 0) * 397) ^ (SourceCity != null ? SourceCity.GetHashCode() : 0);
            }
        }

        public override string ToString() => $"{nameof(SourceCity)}={SourceCity},{nameof(DestinationCity)}={DestinationCity}";
    }
}