using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VuforiaWebService.Api.Core.Util
{
    /// <summary>
    /// Repeatable class which allows you to both pass a single element, as well as an array, as a parameter value.
    /// </summary>
    public class Repeatable<T> : IEnumerable<T>, IEnumerable
    {
        private readonly IList<T> values;

        /// <summary>Creates a repeatable value.</summary>
        public Repeatable(IEnumerable<T> enumeration)
        {
            this.values = new ReadOnlyCollection<T>(new List<T>(enumeration));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>Converts the single element into a repeatable.</summary>
        public static implicit operator Repeatable<T>(T elem)
        {
            if (elem == null)
                return null;
            return new Repeatable<T>(new T[1] { elem });
        }

        /// <summary>Converts a number of elements into a repeatable.</summary>
        public static implicit operator Repeatable<T>(T[] elem)
        {
            if (elem.Length == 0)
                return null;
            return new Repeatable<T>(elem);
        }

        /// <summary>Converts a number of elements into a repeatable.</summary>
        public static implicit operator Repeatable<T>(List<T> elem)
        {
            return new Repeatable<T>(elem);
        }
    }
}
