using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionHandler : MonoBehaviour
{
    private object gameObjects;

    void CollisionHandler()
{
}
public void ~CollisionHandler()
{
	OnDestroy();
}

 void GetInstance()
{
	if (collisionInstance.get() == nullptr)
	{
		collisionInstance.reset(new CollisionHandler);
	}
	return collisionInstance.get();
}

void OnDestroy()
{
	for (auto prev : prevCollisions)
	{
		prev = nullptr;
	}
	prevCollisions.clear();
}

void OnCreate(float worldSize_)
{
	prevCollisions.clear();
}

void AddObject(GameObject* go_)
{
	gameObjects.push_back(go_);
}

void RemoveObject(int location_)
{
	gameObjects.erase(gameObjects.begin() + location_);
}

void AABB()
{

	{
		if (!gameObjects[i]->IsStatic())
		{
			{
				if (gameObjects[i]->GetBoundingBox().Intersects(&gameObjects[j]->GetBoundingBox()) && gameObjects[j]->IsRigid())
				{
					gameObjects[i]->CollisionResponse(gameObjects[j]);
				}
			}
		}
	}
}
}
