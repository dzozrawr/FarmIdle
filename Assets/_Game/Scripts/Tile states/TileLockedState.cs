using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLockedState : TileState
{
    public TileLockedState(TileController tc) : base(tc)
    {
    }
    public override TileState NextState()
    {
        return new TileStartingState(tileController);
    }

    public override void OnHit()
    {
        return;
    }
}
