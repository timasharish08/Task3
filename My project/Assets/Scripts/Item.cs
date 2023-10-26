using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    public event UnityAction<Item> ObjectDestroyed;

    private void OnDestroy()
    {
        if (ObjectDestroyed != null)
            ObjectDestroyed.Invoke(this);
    }
}
