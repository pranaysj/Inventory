using UnityEngine;

public static class GameObjectExtensions
{
    // Finds the child of child GameObject by name
    public static GameObject FindChildOfChildByName(this GameObject parent, string childName)
    {
        foreach (Transform child in parent.transform)
        {
            foreach (Transform grandChild in child)
            {
                if (grandChild.name == childName)
                    return grandChild.gameObject;
            }
        }
        return null;
    }
}
