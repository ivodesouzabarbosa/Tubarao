using UnityEngine;

public class BaseInimigo : ObjectPool
{
    public static BaseInimigo _baseInimigo;
    protected override void Awake()
    {
        base.Awake();
        _baseInimigo = this;
    }
}
