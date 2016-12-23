using UnityEngine;
using System.Collections;

public class TreasureChestSounds : MonoBehaviour {

    TreasureChest TC;
	// Use this for initialization
	void Awake () {
        TC = GetComponentInParent<TreasureChest>();
	}
	
public void PlayChain()
    {
        TC.PlayChainSound();
    }
    public void PlayOpen()
    {
        TC.PlayOpenSound();
    }
    public void PlayLockBreak()
    {
        TC.PlayLockSound();
    }
}
