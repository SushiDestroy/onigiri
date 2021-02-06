﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class IfIntEvent : MonoBehaviour
{
    public IntVariable toCompare;

    public bool onStart = true;
    public bool isSilent = false;

    public ComparePack[] packs;

    private void OnEnable()
    {
        if (toCompare == null)
        {
            Debug.LogError("IntVariable Missing", this);
            return;
        }
        if (!isSilent) toCompare.onChange.AddListener(Compare);
        if (!onStart) return;
        Compare();
    }

    private void OnDisable()
    {
        if (!isSilent) toCompare.onChange.RemoveListener(Compare);
    }

    public void Compare()
    {
        if (toCompare == null)
        {
            Debug.LogError("IntVariable Missing", this);
            return;
        }
        CompareWith(toCompare.Value);
    }

    public void CompareWith(int _v)
    {
        foreach (ComparePack item in packs)
        {
            bool _result = item.Test(_v);

            if (_result) item.onTrue.Invoke();
            else item.onFalse.Invoke();

            item.dynamicResponse.Invoke(item.isInversed ? !(_result) : _result);
        }
    }

    public enum CompareOperation { equals, less, more, lessEqual, moreEqual, different }
    [System.Serializable]
    public class ComparePack
    {
        public int test;
        [HideLabel]
        public CompareOperation how = CompareOperation.equals;
        public UnityEvent onTrue, onFalse;
        public bool isInversed;
        public BoolUnityEvent dynamicResponse;

        public bool Test(int _v)
        {
            switch (how)
            {
                case CompareOperation.equals:
                    return test == _v;
                case CompareOperation.less:
                    return test > _v;
                case CompareOperation.more:
                    return test < _v;
                case CompareOperation.lessEqual:
                    return test >= _v;
                case CompareOperation.moreEqual:
                    return test <= _v;
                case CompareOperation.different:
                    return test != _v;
                default:
                    return false;
            }
        }
    }
}