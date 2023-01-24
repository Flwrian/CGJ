using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckDestroy
{
    public static bool IsNullOrDestroyed( System.Object obj) {
        if (object.ReferenceEquals(obj, null)) return true;
        if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;
        return false;
    }
}
