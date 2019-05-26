namespace OLBIL.Common
{
    public class AgeDescriptor
    {
        public int Years { get; private set; }
        public int Months { get; private set; }
        public int Days { get; private set; }

        public AgeDescriptor(int years, int months, int days)
        {
            Years = years;
            Months = months;
            Days = days;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            AgeDescriptor other = obj as AgeDescriptor;
            if (other == null)
            {
                return false;
            }

            return (Years == other.Years)
                && (Months == other.Months)
                && (Days == other.Days);
        }

        public bool Equals(AgeDescriptor other)
        {
            if (other == null)
            {
                return false;
            }

            return (Years == other.Years)
                && (Months == other.Months)
                && (Days == other.Days);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return 13 ^ Years + 7 ^ Days + 3 ^ Months ;
        }

        public static bool operator ==(AgeDescriptor a, AgeDescriptor b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return (a.Years == b.Years)
                            && (a.Months == b.Months)
                            && (a.Days == b.Days);
        }

        public static bool operator !=(AgeDescriptor a, AgeDescriptor b)
        {
            return !(a == b);
        }
    }
}
