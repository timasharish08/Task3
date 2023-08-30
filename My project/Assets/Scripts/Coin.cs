using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<Coin> ObjectDestroyed;

    private void OnDestroy()
    {
        if (ObjectDestroyed != null)
            ObjectDestroyed.Invoke(this);
    }
}
