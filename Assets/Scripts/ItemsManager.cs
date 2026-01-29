using UnityEngine;
using System.Collections.Generic;

public class ItemsManager : MonoBehaviour
{
    public List<GameObject> itemsInHands = new List<GameObject>();
    public int currentIndex = 0;
    private bool itemHidden = false;

    void Start()
    {
        // Pokaži samo trenutni item
        ShowCurrentItemOnly();
    }

    void Update()
    {
        // Switch items z 1,2,3
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchToItem(2);

        // Toggle X → skrije/prikaže samo trenutni item
        if (Input.GetKeyDown(KeyCode.X))
        {
            itemHidden = !itemHidden;
            if (itemHidden)
                itemsInHands[currentIndex].SetActive(false);
            else
                itemsInHands[currentIndex].SetActive(true);
        }
    }

    public void PickUpItem(GameObject newItem)
    {
        if (!itemsInHands.Contains(newItem))
            itemsInHands.Add(newItem);

        currentIndex = itemsInHands.IndexOf(newItem);

        if (!itemHidden)
            ShowCurrentItemOnly();
    }

    private void SwitchToItem(int index)
    {
        if (index >= 0 && index < itemsInHands.Count)
        {
            // Skrij prejšnji item
            itemsInHands[currentIndex].SetActive(false);

            currentIndex = index;

            // pokaži samo če toggle ni skrit
            if (!itemHidden)
                itemsInHands[currentIndex].SetActive(true);
        }
    }

    private void ShowCurrentItemOnly()
    {
        for (int i = 0; i < itemsInHands.Count; i++)
        {
            itemsInHands[i].SetActive(i == currentIndex && !itemHidden);
        }
    }
}
