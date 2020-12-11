﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public float SetPosition;

public float SetRotation;

public float GetPosition;

public float GetOrthographic;

public float GetPerspective;

public float GetView;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



	Camera::Camera() : position(glm::vec3())
	{
		fieldOfView = 45.0ff;
		forward = glm::vec3(0, 0, 1.0ff);
		up = glm::vec3(0, 1.0ff, 0);
		worldUp = up;
		nearPlane = 2.0ff;
		farPlane = 50.0ff;
		yaw = -90.0ff;
		pitch = 0.0ff;

		perspective = glm::perspective(fieldOfView,
			CoreEngine::GetInstance()->GetWindowSize().x /
			CoreEngine::GetInstance()->GetWindowSize().y,
			nearPlane, farPlane);

		orthographic = glm::ortho(0.0ff, CoreEngine::GetInstance()->GetWindowSize().x,
			0.0ff, CoreEngine::GetInstance()->GetWindowSize().y, -1.0ff, 1.0ff);

		UpdateCameraVectors();
	}

	Camera::~Camera()
	{
		OnDestroy();
	}

	void Camera::SetPosition(glm::vec3 position_)
	{
		position = position_;
		UpdateCameraVectors();
	}

	void Camera::SetRotation(float yaw_, float pitch_)
	{
		yaw = yaw_;
		pitch = pitch_;
		UpdateCameraVectors();
	}

	void Camera::Translate(const glm::vec3 movement_)
{
	SetPosition(position + movement_);
}

glm::mat4 Camera::GetView() const
{
	return glm::lookAt(position, position + forward, up);
}

glm::mat4 Camera::GetPerspective() const
{
	return perspective;
}

glm::mat4 Camera::GetOrthographic() const
{
	return orthographic;
}

glm::vec3 Camera::GetPosition() const
{
	return position;
}

glm::vec2 Camera::GetClippingPlanes() const
{
	return glm::vec2(nearPlane, farPlane);
}

void Camera::ProcessMouseMovement(glm::vec2 offset_)
{

	UpdateCameraVectors();
}

void Camera::ProcessMouseScroll(int y_)
{
	if (y_ < 0 || y_ > 0)
	{
		position += static_cast<float>(y_) * (forward * 2.0ff);
	}
	UpdateCameraVectors();
}

std::vector<glm::vec4> Camera::GetFrustumPlanes()
{
	std::vector<glm::vec4> frustum;
	frustum.reserve(6);

	glm::mat4 perp = GetView() * GetPerspective();

	//Left
	frustum.push_back(glm::vec4(perp[3].x + perp[0].x,
		perp[3].y + perp[0].y,
		perp[3].z + perp[0].z,
		perp[3].w + perp[0].w));

	//Right
	frustum.push_back(glm::vec4(perp[3].x - perp[0].x,
		perp[3].y - perp[0].y,
		perp[3].z - perp[0].z,
		perp[3].w - perp[0].w));

	//Top
	frustum.push_back(glm::vec4(perp[3].x - perp[1].x,
		perp[3].y - perp[1].y,
		perp[3].z - perp[1].z,
		perp[3].w - perp[1].w));

	//Bottom
	frustum.push_back(glm::vec4(perp[3].x + perp[1].x,
		perp[3].y + perp[1].y,
		perp[3].z + perp[1].z,
		perp[3].w + perp[1].w));

	//Near
	frustum.push_back(glm::vec4(perp[2].x, perp[2].y, perp[2].z, perp[2].w));

	//Far
	frustum.push_back(glm::vec4(perp[3].x - perp[2].x,
		perp[3].y - perp[2].y,
		perp[3].z - perp[2].z,
		perp[3].w - perp[2].w));

	for (auto frustum frust  )
	{
		float length = frust.length();
		frust.x /= length;
		frust.y /= length;
		frust.z /= length;
		frust.w /= length;
	}

	return frustum;
}

//AABB culling
bool Camera::FrustumCull(std::vector<glm::vec4> frustum_, BoundingBox* box_)
{

	for (int i = 0; i < 6; i++)
	{

		glm::vec4 planeNormal = glm::vec4(frustum_[i].x, frustum_[i].y, frustum_[i].z, 0.0ff);
		float planeConst = frustum_[i].w;

		glm::vec3 axisVert;

		//ok so i need to get game object world position in order for this to work

		// x-axis
		if (frustum_[i].x < 0.0ff) {
		axisVert.x = box_->pos.x;
	}
		else
	{
		axisVert.x = box_->dimentions.x + box_->pos.x;
	}

	// y-axis
	if (frustum_[i].y < 0.0ff) {
		axisVert.y = box_->pos.y;
	}
		else
	{
		axisVert.y = box_->dimentions.y + box_->pos.y;
	}

	//Is point outside of frustum
	if (glm::dot(glm::vec3(planeNormal), axisVert) + planeConst < 0.0ff) {
		return true;
	}
}

return false;
}

void Camera::UpdateCameraVectors()
{
	forward.x = cos(glm::radians(yaw)) * cos(glm::radians(pitch));
	forward.y = sin(glm::radians(pitch));
	forward.z = sin(glm::radians(yaw)) * cos(glm::radians(pitch));

	forward = glm::normalize(forward);

	right = glm::normalize(glm::cross(forward, worldUp));

	up = glm::normalize(glm::cross(right, forward));
}

void Camera::OnDestroy()
{

}


}










using UnityEngine;
using System.Collections;

public class MYCLASSNAME : MonoBehaviour
{
# include "Camera.h"
# include "../Core/CoreEngine.h"


	Camera::Camera() : position(glm::vec3())
	{
		fieldOfView = 45.0ff;
		forward = glm::vec3(0, 0, 1.0ff);
		up = glm::vec3(0, 1.0ff, 0);
		worldUp = up;
		nearPlane = 2.0ff;
		farPlane = 50.0ff;
		yaw = -90.0ff;
		pitch = 0.0ff;

		perspective = glm::perspective(fieldOfView,
			CoreEngine::GetInstance()->GetWindowSize().x /
			CoreEngine::GetInstance()->GetWindowSize().y,
			nearPlane, farPlane);

		orthographic = glm::ortho(0.0ff, CoreEngine::GetInstance()->GetWindowSize().x,
			0.0ff, CoreEngine::GetInstance()->GetWindowSize().y, -1.0ff, 1.0ff);

		UpdateCameraVectors();
	}

	Camera::~Camera()
	{
		OnDestroy();
	}

	void Camera::SetPosition(glm::vec3 position_)
	{
		position = position_;
		UpdateCameraVectors();
	}

	void Camera::SetRotation(float yaw_, float pitch_)
	{
		yaw = yaw_;
		pitch = pitch_;
		UpdateCameraVectors();
	}

	void Camera::Translate(const glm::vec3 movement_)
{
	SetPosition(position + movement_);
}

glm::mat4 Camera::GetView() const
{
	return glm::lookAt(position, position + forward, up);
}

glm::mat4 Camera::GetPerspective() const
{
	return perspective;
}

glm::mat4 Camera::GetOrthographic() const
{
	return orthographic;
}

glm::vec3 Camera::GetPosition() const
{
	return position;
}

glm::vec2 Camera::GetClippingPlanes() const
{
	return glm::vec2(nearPlane, farPlane);
}

void Camera::ProcessMouseMovement(glm::vec2 offset_)
{
	/*
	offset_ *= 0.05ff;

	yaw += offset_.x;
	pitch += offset_.y;

	if (pitch > 89.0ff) {
		pitch = 89.0ff;
	}
	if (pitch < -89.0ff) {
		pitch = 89.0ff;
	}

	if (yaw < 0.0ff) {
		yaw += 360.0ff;
	}

	if (yaw > 360.0ff) {
		yaw -= 360.0ff;
	}
	*/
	UpdateCameraVectors();
}

void Camera::ProcessMouseScroll(int y_)
{
	if (y_ < 0 || y_ > 0)
	{
		position += static_cast<float>(y_) * (forward * 2.0ff);
	}
	UpdateCameraVectors();
}

std::vector<glm::vec4> Camera::GetFrustumPlanes()
{
	std::vector<glm::vec4> frustum;
	frustum.reserve(6);

	glm::mat4 perp = GetView() * GetPerspective();

	//Left
	frustum.push_back(glm::vec4(perp[3].x + perp[0].x,
		perp[3].y + perp[0].y,
		perp[3].z + perp[0].z,
		perp[3].w + perp[0].w));

	//Right
	frustum.push_back(glm::vec4(perp[3].x - perp[0].x,
		perp[3].y - perp[0].y,
		perp[3].z - perp[0].z,
		perp[3].w - perp[0].w));

	//Top
	frustum.push_back(glm::vec4(perp[3].x - perp[1].x,
		perp[3].y - perp[1].y,
		perp[3].z - perp[1].z,
		perp[3].w - perp[1].w));

	//Bottom
	frustum.push_back(vec4(perp[3].x + perp[1].x,
		perp[3].y + perp[1].y,
		perp[3].z + perp[1].z,
		perp[3].w + perp[1].w));

	//Near
	frustum.push_back(glm::vec4(perp[2].x, perp[2].y, perp[2].z, perp[2].w));

	//Far
	frustum.push_back(glm::vec4(perp[3].x - perp[2].x,
		perp[3].y - perp[2].y,
		perp[3].z - perp[2].z,
		perp[3].w - perp[2].w));

	for (auto frustum frust  )
	{
		float length = frust.length();
		frust.x /= length;
		frust.y /= length;
		frust.z /= length;
		frust.w /= length;
	}

	return frustum;
}

//AABB culling
bool Camera::FrustumCull(std::vector<glm::vec4> frustum_, BoundingBox* box_)
{

	for (int i = 0; i < 6; i++)
	{

		glm::vec4 planeNormal = glm::vec4(frustum_[i].x, frustum_[i].y, frustum_[i].z, 0.0ff);
		float planeConst = frustum_[i].w;

		glm::vec3 axisVert;

		//ok so i need to get game object world position in order for this to work

		// x-axis
		if (frustum_[i].x < 0.0ff) {
		axisVert.x = box_->pos.x;
	}
		else
	{
		axisVert.x = box_->dimentions.x + box_->pos.x;
	}

	// y-axis
	if (frustum_[i].y < 0.0ff) {
		axisVert.y = box_->pos.y;
	}
		else
	{
		axisVert.y = box_->dimentions.y + box_->pos.y;
	}

	//Is point outside of frustum
	if (glm::dot(glm::vec3(planeNormal), axisVert) + planeConst < 0.0ff) {
		return true;
	}
}

return false;
}

void Camera::UpdateCameraVectors()
{
	forward.x = cos(glm::radians(yaw)) * cos(glm::radians(pitch));
	forward.y = sin(glm::radians(pitch));
	forward.z = sin(glm::radians(yaw)) * cos(glm::radians(pitch));

	forward = glm::normalize(forward);

	right = glm::normalize(glm::cross(forward, worldUp));

	up = glm::normalize(glm::cross(right, forward));
}

void Camera::OnDestroy()
{

}
}







using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour
{
	// Start is called before the first frame update
	private void Move()
	{
		float movementInput = playerActionControls.Land.MoveReadValue<float>();

		Vector3 curentPosition = transform.position;
		curentPosition.x += movementInput * speed * Time.deltaTime;
		transform.position = curentPosition;

		if (movementInput != 0) animator.SetBool("Run", true);
		else animator.SetBool("Run", false);

		if (movementInput == -1)
			spriteRenderer.flipX = true;

		else if (movementInput == 1)
			spriteRenderer.flipX = false;
	}

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enenmy")
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene());
        }


    }

}

}