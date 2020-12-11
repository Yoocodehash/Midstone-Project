using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public void ~Spike()
    {

    }

    bool OnCreate(vec2 pos_)
    {
        SetPosition(pos_);

        SetStatic(true);

        return false;
    }

    void Update(const float deltaTime_)
{

}
