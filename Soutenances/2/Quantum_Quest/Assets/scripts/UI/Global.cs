using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {

    public static SlotManager slotMgr;

    void Start()
    {
        gameObject.GetComponentInChildren<InventoryManager>().ToogleInventory(false);
        slotMgr.CreateSlot();
        gameObject.GetComponentInChildren<InventoryManager>().ToogleInventory(false);
    }
}
