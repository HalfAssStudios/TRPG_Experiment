using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStat : ScriptableObject
{
    public float m_runSpeed = 5.0f;

    public void DoubleRunSpeed()
    {
        m_runSpeed += .002f;
    }
}
