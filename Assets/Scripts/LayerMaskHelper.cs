using UnityEngine;

public static class LayerMaskHelper
{
    public static bool GameObjectIsOnLayerMask(GameObject gameObject, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << gameObject.layer));
    }

    public static LayerMask Everything => ~0;
}
