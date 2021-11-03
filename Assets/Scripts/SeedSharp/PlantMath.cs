using System;
using UnityEngine;

namespace SeedSharp
{
    public static class PlantMath
    {
        public const float PI = (float) Math.PI;
        public static float Pow(float x, float y) => Mathf.Pow(x, y);
        public static float Cos(float x) => Mathf.Cos(x);
        public static float Log(float f, float p) => Mathf.Log(f, p);
        public static float Log2(float f) => Log(f, 2);
        public static float Ceil(float x) => Mathf.Ceil(x);
    }
}