using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<Coin> ObjectDestroyed;

    private void OnDestroy()
    {
        ObjectDestroyed.Invoke(this);
    }
}
