using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private OrderSlotUI slotPrefab;
    [SerializeField] private Transform slotContainer;

    private bool isOpen = false;

    void Start()
    {
        rootPanel.SetActive(false);
        GenerateSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        rootPanel.SetActive(isOpen);
    }

    public void Close()
    {
        isOpen = false;
        rootPanel.SetActive(false);
    }

    private void GenerateSlots()
    {
        if (itemDatabase == null)
        {
            Debug.LogError("ItemDatabase is NULL!");
            return;
        }

        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in itemDatabase.items)
        {
            OrderSlotUI slot = Instantiate(slotPrefab, slotContainer);
            slot.Setup(item);
        }
    }
}