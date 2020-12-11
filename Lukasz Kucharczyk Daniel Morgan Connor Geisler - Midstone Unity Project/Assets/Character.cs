using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

public void ~Character()
	{

	}

	public bool OnCreate()
	{
		SetGravity(true);
		SetRigid(true);
		return true;
	}

	private void SetGravity(bool v)
	{
		throw new NotImplementedException();
	}

	private void SetRigid(bool v)
	{
		throw new NotImplementedException();
	}

	public void Update(const float deltaTime_)
		{
			Update(deltaTime_);
		}

public void IsDead()
{
	return dead;
}

public void Kill()
{
	dead = true;
}

 void LoadMods()
{
	["Movement"] = false;
	["Jump"] = false;
	["Shoot"] = false;
	["Flight"] = false;
}

 void GetMod(string name_)
{
	return [name_];
}


 void CollisionResponse(GameObject* obj)
{
    CollisionResponse(obj);
}


}