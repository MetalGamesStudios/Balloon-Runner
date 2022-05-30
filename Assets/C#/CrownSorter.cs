using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownSorter : MonoBehaviour
{
    [SerializeField] public Transform[] runners;
    [SerializeField] private float myZ;

    [SerializeField] private float _leastPos = 1000;
    [SerializeField] private Transform _lastOwnerTr;
    [SerializeField] private Crown _LastOwnerCrownSCript;

    private void Awake()
    {
        myZ = transform.position.z;
        InvokeRepeating(nameof(Sort), 1, 1);
    }

    public void Sort()
    {
        foreach (Transform runner in runners)
        {
            var result = myZ - runner.position.z;
            if (result < _leastPos)
            {
                if (runner == _lastOwnerTr)
                    return;

                _leastPos = result;
                _lastOwnerTr = runner;

                var script = _lastOwnerTr.GetComponentInChildren<FiringController>().crown;
                script.Show();
                if (_LastOwnerCrownSCript)
                    _LastOwnerCrownSCript.Hide();
                _LastOwnerCrownSCript = script;
            }
        }
    }
}