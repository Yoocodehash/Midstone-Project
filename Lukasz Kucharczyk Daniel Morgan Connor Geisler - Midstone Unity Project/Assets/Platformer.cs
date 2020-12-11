using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{

    void Platformer()
{
}

    void ~Platformer()
{
}

    bool Initilize()
{
        SetMod("Movement", true);
        SetMod("Jump", true);
        SetMod("Shoot", true);

    return true;
}

 void Update(const float deltaTime_)
{
}


}