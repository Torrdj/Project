using UnityEngine;
using System.Collections;

public class SlotManager : MonoBehaviour {

    public int columns = 0;
    public int rows = 0;
    public float offset = 0;
    public Vector3 slotPos;
    public GameObject slotPrefab;

    void Awake()
    {
        Global.slotMgr = this;
    }

    public void CreateSlot()
    {
        Vector3 backupPos = slotPos;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject slot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                slot.transform.SetParent(this.transform);
                slot.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);

                RectTransform slotTransform = slot.GetComponent<RectTransform>();
                slotTransform.localPosition = new Vector3(slotPos.x, slotPos.y, 0);

                slotPos.x += offset;
            }
            slotPos.x = backupPos.x;
            slotPos.y -= offset;
        }
    }
}
