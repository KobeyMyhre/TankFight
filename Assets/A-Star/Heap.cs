using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
        currentItemCount = 0;
    }
	
    public void Add(T item)
    {
        item.heapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public bool Contains(T item)
    {
        return Equals(items[item.heapIndex], item);
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    void sortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = (item.heapIndex * 2) + 1;
            int childIndexRight = (item.heapIndex * 2) + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if(childIndexRight < currentItemCount)
                {
                    if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if(item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else { return; }
               
            }
            else { return; }
        }
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        if(items[0] != null)
        {
            items[0].heapIndex = 0;
            sortDown(items[0]);
        }
        
        return firstItem;
    }

    void SortUp(T item)
    {
        int parentIdx = (item.heapIndex - 1) / 2;
        while(true)
        {
            T parentItem = items[parentIdx];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else { break; }
            parentIdx = (item.heapIndex - 1) / 2;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.heapIndex] = itemB;
        items[itemB.heapIndex] = itemA;
        int itemIndex = itemA.heapIndex;
        itemA.heapIndex = itemB.heapIndex;
        itemB.heapIndex = itemIndex;
    }

}
public interface IHeapItem<T> : IComparable<T>
{
    int heapIndex
    {
        get;
        set;
    }
}

