using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    [SerializeField] GameObject heartContainerPrefab;
    [SerializeField] List<GameObject> heartContainers;
    [SerializeField] int totalHearts;
    [SerializeField] int currentHearts;
    [SerializeField] HeartContainer currentContainer;
    void Start()
    {
        instance = this;
        heartContainers = new List<GameObject>();
        SetupHearts(5);
    }

    public void SetupHearts(int heartsIn)
    {
        heartContainers.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        totalHearts = heartsIn;
        currentHearts = totalHearts;

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainerPrefab, transform);
            heartContainers.Add(newHeart);
            if (currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartContainer>();
            }
            currentContainer = newHeart.GetComponent<HeartContainer>();
        }
        currentContainer = heartContainers[0].GetComponent<HeartContainer>();
    }

    public void SetCurrentHealth(int health)
    {
        currentHearts = health;
        currentContainer.SetHeart(currentHearts);
    }
    public void AddHearts(int health)
    {
        currentHearts += health;
        if (currentHearts > totalHearts)
        {
            currentHearts = totalHearts;
        }
        currentContainer.SetHeart(currentHearts);
    }
    public void RemoveHearts(int health)
    {
        currentHearts -= health;
        if (currentHearts < 0)
        {
            currentHearts = 0;
        }
        currentContainer.SetHeart(currentHearts);
    }

    public void AddContainer()
    {
        GameObject newHeart = Instantiate(heartContainerPrefab, transform);
        currentContainer = heartContainers[heartContainers.Count - 1].GetComponent<HeartContainer>();
        heartContainers.Add(newHeart);

        if (currentContainer != null)
        {
            currentContainer.next = newHeart.GetComponent<HeartContainer>();
        }
        currentContainer = heartContainers[0].GetComponent<HeartContainer>();

        totalHearts++;
        currentHearts = totalHearts;
        SetCurrentHealth(currentHearts);

    }
}
