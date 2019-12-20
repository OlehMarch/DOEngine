#version 400

layout (location = 0) in vec3 vertex;

uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;

out vec3 texVector;

void main(void){

	vec3 worldPos = vertex;
	texVector = worldPos;
	gl_Position = projectionMatrix * viewMatrix * vec4(worldPos, 1.0);
}