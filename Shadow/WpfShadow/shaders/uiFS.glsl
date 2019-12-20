#version 400

layout (location = 0) out vec4 FragColor;

uniform sampler2D texImage;

in vec2 texCoord;

void main(){

	FragColor = texture(texImage, texCoord);
}
