using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileState
{
    protected TileController tileController = null;
    public TileState(TileController tc)
    {
        tileController = tc;
    }
    public abstract void OnHit();

    public abstract TileState NextState();
}
