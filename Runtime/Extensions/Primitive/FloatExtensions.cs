namespace UtilsToolbox.Extensions.Primitive
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Remaps value from one numerical range to other
        /// </summary>
        /// <param name="value"> value to remap </param>
        /// <param name="fromMin"> source numerical range first value </param>
        /// <param name="fromMax"> source numerical range last value </param>
        /// <param name="toMin"> target numerical range first value </param>
        /// <param name="toMax"> target numerical range last value </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            if (UnityEngine.Mathf.Approximately(fromMax, fromMin))
            {
                throw new System.ArgumentException("Input range cannot have a length of 0!");
            }

            return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
        }
        
        /// <summary>
        /// Remaps value from one numerical range to [0, 1]
        /// </summary>
        /// <param name="value"> value to remap </param>
        /// <param name="fromMin"> source numerical range first value </param>
        /// <param name="fromMax"> source numerical range last value </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static float Remap01(this float value, float fromMin, float fromMax)
        {
            if (UnityEngine.Mathf.Approximately(fromMax, fromMin))
            {
                throw new System.ArgumentException("Input range cannot have a length of 0!");
            }

            return (value - fromMin) / (fromMax - fromMin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float Squared(this float value)
        {
            return value * value;
        }
    }
}