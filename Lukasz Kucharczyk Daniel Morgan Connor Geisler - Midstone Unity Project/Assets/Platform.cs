using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void ~Platform()
    {
    }

    bool OnCreate()
    {
        SetStatic(true);
        SetRigid(true);
        SetScale(vec2(10.0f));

        return true;
    }

   public void Update(const float deltaTime_)
{

}



}
