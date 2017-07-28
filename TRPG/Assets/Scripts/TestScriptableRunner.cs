using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptableRunner : MonoBehaviour {

    public RunStat runstat;

    public void Start()
    {
    }

    public void Update()
    {
        transform.position = transform.position + (transform.forward * Time.deltaTime * runstat.m_runSpeed);

        runstat.m_runSpeed = 10;
    }
}
