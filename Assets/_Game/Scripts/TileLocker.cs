using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLocker : MonoBehaviour
{
    public bool isLocked = false;   //all the tiles are unlocked by default

    public Canvas lockedUI = null;

    private List<TileController> tileControllers = null;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocked)
        {
            lockedUI.gameObject.SetActive(true);
            if (tileControllers == null) tileControllers = new List<TileController>();
            foreach (TileController tc in GetComponentsInChildren<TileController>())
            {
                tileControllers.Add(tc);
                tc.Lock();
            }
        }
    }

    public void Unlock()
    {
        lockedUI.gameObject.SetActive(false);

        foreach (TileController tc in tileControllers)
        {
            tc.Unlock();
        }
    }
}
