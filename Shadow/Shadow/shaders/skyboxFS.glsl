#version 400

layout (location = 0) out vec4 FragColor;

uniform samplerCube cubeTex;

in vec3 texVector;

void main(void){

	FragColor = texture(cubeTex, texVector);
}