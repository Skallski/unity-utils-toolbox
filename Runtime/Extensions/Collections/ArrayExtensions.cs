namespace UtilsToolbox.Extensions.Collections
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return System.Array.IndexOf(array, value);
        }  
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="match"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int FindIndex<T>(this T[] array, System.Predicate<T> match)
        {
            return System.Array.FindIndex(array, match);
        }
        
        /// <summary>
        /// Calls action on each array element
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <param name="onElementAction"> action that is performed on each array element</param>
        /// <typeparam name="T"> type of array element </typeparam>
        public static void ForEach<T>(this T[] array, System.Action<T> onElementAction)
        {
            System.Array.ForEach(array, onElementAction);
        }

        /// <summary>
        /// Creates a subset of an array
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <param name="startIndex"> index that will be the first element of subset </param>
        /// <param name="endIndex"> index that will be the last element of subset </param>
        /// <typeparam name="T">  type of the array </typeparam>
        /// <returns> array, which is a subset of original processed array </returns>
        public static T[] Subset<T>(this T[] array, int startIndex, int endIndex)
        {
            return new System.ArraySegment<T>(array, startIndex, endIndex).ToArray();
        }
        
        /// <summary>
        /// Gets random item from array
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <typeparam name="T"> type of array </typeparam>
        /// <returns> random item from array </returns>
        public static T GetRandomItem<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                throw new System.IndexOutOfRangeException("Cannot select random item from empty array!");
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        /// <summary>
        /// Shuffles list
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <typeparam name="T"> type of array </typeparam>
        public static void Shuffle<T>(this T[] array)
        {
            int len = array.Length;
            for (int i = len - 1; i > 1; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (array[j], array[i]) = (array[i], array[j]);
            }
        }

        /// <summary>
        /// Gets index of the last element inside the array
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <typeparam name="T"> type of array </typeparam>
        /// <returns> int value that is the index of the last element inside the array </returns>
        public static int GetLastElementIndex<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                throw new System.IndexOutOfRangeException("Cannot select random item from empty array!");
            }
            
            return array.Length - 1;
        }

        /// <summary>
        /// Gets first array element or default
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <typeparam name="T"> type of array </typeparam>
        /// <returns> first array element or default value </returns>
        public static T FirstOrDefault<T>(this T[] array)
        {
            return array[0] ?? default;
        }
        
        /// <summary>
        /// Gets last array element or default
        /// </summary>
        /// <param name="array"> array on which the method will be called </param>
        /// <typeparam name="T"> type of array </typeparam>
        /// <returns> first array element or default value </returns>
        public static T LastOrDefault<T>(this T[] array)
        {
            return array[^1] ?? default;
        }
    }
}