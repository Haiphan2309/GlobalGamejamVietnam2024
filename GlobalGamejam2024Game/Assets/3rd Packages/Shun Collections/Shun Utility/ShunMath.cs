using UnityEngine;

namespace Shun_Utility
{
    public static class ShunMath
    {
        /// <summary>
        /// return 1 when positive
        /// return 0 when zero
        /// return -1 when negative
        /// </summary>
        public static int GetSignOrZero(float value)
        {
            return value > 0 ? 1 : (value < 0 ? -1 : 0);
        }
        
        public static Vector3 MultiplyVector3(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.x * vector2.x, vector1.y * vector2.y, vector1.z * vector2.z);
        }
    }
}