#version 400

layout (location = 0) in vec3 vertex;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 texCoord;
layout (location = 4) in vec3 tangent;
layout (location = 5) in vec3 bitangent; 

uniform mat4 modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;
uniform mat4 shadowMatrix;
uniform vec3 lightDir;
uniform vec3 cameraPos;

out vec2 texCoords;
out vec3 camera;
out vec3 lightV;
out vec4 shadowCoord;

void main(void){

	shadowCoord = shadowMatrix * vec4(vertex, 1.0);
	texCoords = texCoord;
	vec4 eyePos = viewMatrix * modelMatrix * vec4(vertex, 1.0);

	vec3 normWorld = normalize((modelMatrix * vec4(normal, 0.0)).xyz);
	vec3 tangWorld = normalize((modelMatrix * vec4(tangent, 0.0)).xyz);
	vec3 bitangWorld = normalize((modelMatrix * vec4(bitangent, 0.0)).xyz);

	mat3 tangSpace = mat3(
		tangWorld.x, bitangWorld.x, normWorld.x, 
		tangWorld.y, bitangWorld.y, normWorld.y, 
		tangWorld.z, bitangWorld.z, normWorld.z
	);
	
	camera = tangSpace * (cameraPos - eyePos.xyz);
	lightV = tangSpace * lightDir;

	gl_Position = projectionMatrix * eyePos;
}