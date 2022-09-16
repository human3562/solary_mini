using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ScrollingBuffer
{
    public int MaxSize;
    public int Offset;
    public List<Vector3> Data;

    public ScrollingBuffer(int max_size = 2000) {
        MaxSize = max_size;
        Offset = 0;
        Data = new List<Vector3>();
    }
    
    public void AddPoint(Vector3 pos) {
        if(Data.Count < MaxSize) {
            Data.Add(pos);
        }
        else {
            Data[Offset] = pos;
            Offset = (Offset + 1) % MaxSize;
        }
    }

    public void Erase() {
        if(Data.Count > 0) {
            Data.Clear();
            Offset = 0;
        }
    }
}
