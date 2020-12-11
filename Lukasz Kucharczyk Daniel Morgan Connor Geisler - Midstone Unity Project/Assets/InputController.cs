using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

	public void InputControl() {
		player = nullptr;
	}

public void Init(Character* player_)
{
	player = player_;
}

	public void Update(float deltaTime_)
{

	if (player->GetMod("Movement"))
	{
		if (KeyEventListener::keyMap[SDLK_a] == true)
		{
			player->Flip(true);
			player->Translate(glm::vec2(-2.0f, 0.0f));
		}
		if (KeyEventListener::keyMap[SDLK_s])
		{

		}
		if (KeyEventListener::keyMap[SDLK_d])
		{
			player->Flip(false);
			player->Translate(glm::vec2(2.0f, 0.0f));
		}
		if (KeyEventListener::keyMap[SDLK_w])
		{
		}
		if (KeyEventListener::keyMap[SDLK_SPACE])
		{
			player->Translate(glm::vec2(0, 2.0f));
		}
		if (KeyEventListener::keyMap[SDLK_LSHIFT])
		{
			if (player->GetSprite()->GetFlip() == true)
			{
				player->Translate(glm::vec2(-10.0f, 0.0f));
			}
			else
			{
				player->Translate(glm::vec2(10.0f, 0.0f));
			}
		}
	}
	if (player->GetMod("Shoot"))
	{
		if (KeyEventListener::keyMap[SDLK_f])
		{
			player->Shot();
		}
	}
	if (player->GetMod("Flight"))
	{
		player->ApplyVelocity(glm::vec2(100.0f, 0.0f));
		if (KeyEventListener::keyMap[SDLK_d])
		{
			player->SetRotation(player->GetRotation() + 1);
		}
		if (KeyEventListener::keyMap[SDLK_a])
		{
			player->SetRotation(player->GetRotation() - 1);
		}
	}



}