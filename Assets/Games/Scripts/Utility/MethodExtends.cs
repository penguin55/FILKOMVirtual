using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MethodExtends
{
    public static Vector2 ParseVector2(this Vector3 value)
    {
        return value;
    }

    public static Vector3 ParseVector3(this Vector2 value)
    {
        return value;
    }

    public static Vector3 ParseVector3(this Vector2 value, float zValue)
    {
        return new Vector3(value.x, value.y, zValue);
    }
}
