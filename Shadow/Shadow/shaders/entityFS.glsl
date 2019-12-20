#version 400

layout (location = 0) out vec4 FragColor;

#define shininess 150

uniform sampler2D frameTexture;
uniform sampler2D normalMap;
uniform sampler2D shadowmap;

uniform vec3 ambient;
uniform vec3 diffuse;
uniform vec3 specular;
uniform bool multCoords;

in vec2 texCoords;
in vec3 camera;
in vec3 lightV;
in vec4 shadowCoord;

vec3 directLight(vec3 i, vec3 v, vec3 n){
	
	float d = dot(i, n);
	float diff = max(d, 0.0);
	vec3 diffLight = diffuse * diff;
	vec3 specLight = vec3(0);
	if (d > 0.0){
		
		vec3 halfV = normalize(i + v);
		float s = max(dot(halfV, n), 0.0);
		specLight = pow(s, shininess) * specular * 10;
	}
	
	return diffLight + specLight;
}

void main(void){

	vec3 shadowTexCoord = shadowCoord.xyz / shadowCoord.w;
	vec4 shadowResult = texture(shadowmap, shadowTexCoord.xy);
	float shadow = 1.0;
	if (shadowResult.r < (shadowTexCoord.z - 0.005)){
		shadow = 0.0;
	}

	vec2 texCoord = texCoords;
	if (multCoords){
		texCoord *= 5;
	}


	vec3 incident = normalize(lightV);
	vec3 normal = normalize(((2.0 * texture(normalMap, texCoord)) - 1.0).rgb);
	vec3 toCamera = normalize(camera);
	
	vec3 light = directLight(incident, toCamera, normal);

	vec4 tex = texture(frameTexture, texCoord);
	vec4 result = vec4((light * shadow) + ambient, 1.0) * tex;
	
	FragColor = result;
}