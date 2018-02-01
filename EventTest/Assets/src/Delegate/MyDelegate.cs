using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NoParamDelegate();

public class MyDelegate
{
    public event NoParamDelegate MyEvent;

}
