SamplerState defaultSampler
{
    Filter = MIN_MAG_MIP_POINT;
};

Texture2D lightBuffer;
Texture2D normalBuffer;
Texture2D diffuseBuffer;
//Texture2D depthMap;
//Texture2D lightDepthMap;

matrix OverlayViewProjection;
//matrix InverseProjection;
//matrix LightInverseViewProjection;
float4 SunLightDirection;

struct VS_OUT
{
	float4 Pos : SV_POSITION;
	float2 texCoord : TEXCOORD;
};

VS_OUT VS(uint id : SV_VertexID)
{
	VS_OUT output = (VS_OUT)0;

	// Construct full-screen triangle
	output.texCoord = float2((id << 1) & 2, id & 2);
	output.Pos = float4(output.texCoord * float2(2.0f, -2.0f) + float2(-1.0f, 1.0f), 0.0f, 1.0f);

	return output;
}

float4 PS( VS_OUT input ) : SV_Target
{
	float3 diffuseSample = diffuseBuffer.Sample(defaultSampler, input.texCoord).rgb;
	float4 normalSample = normalBuffer.Sample(defaultSampler, input.texCoord);

	// Skip lighting if normal.w == 0
	if (normalSample.w == 0)
	{
		return float4(diffuseSample, 1);
	}

	float4 lightSample = lightBuffer.Sample(defaultSampler, input.texCoord);
	//float depthSample = depthMap.Sample(defaultSampler, input.texCoord).x;
	//float lightDepthSample = lightDepthMap.Sample(defaultSampler, input.texCoord).x;

	float3 normal = normalize((normalSample.xyz - 0.5) * 2); // from 0...1 to -1...1

	// Ambient term
	float3 ambientColor = float3(0.4, 0.4, 0.4);
	float3 ambient = ambientColor * diffuseSample;

	float3 dirLight = 0.5 * saturate(dot(normal, -SunLightDirection.xyz)) * diffuseSample;

	//float shade *= GetShadowAmount(input.LPos);
	//diffuse *= shade;

	// Debugging
	//return float4(depthSample, depthSample, depthSample, 1);
	//return float4(normal, 1);

	return float4(lightSample.xyz + ambient + dirLight, 1);
}

VS_OUT Overlay_VS(uint id : SV_VertexID)
{
	VS_OUT output = (VS_OUT)0;

	// Construct overlay quad
	output.texCoord = 0.5 * float2((id << 1) & 2, id & 2);
	output.Pos = float4(output.texCoord * float2(2.0f, -2.0f) + float2(-1.0f, 1.0f), 0.0f, 1.0f);
	output.Pos = mul(output.Pos, OverlayViewProjection);

	return output;
}

float4 Overlay_PS( VS_OUT input ) : SV_Target
{
	return diffuseBuffer.Sample(defaultSampler, input.texCoord);
}

technique10 Render
{
	pass P1
	{
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetGeometryShader( NULL );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}

	pass Overlay
	{
		SetVertexShader( CompileShader( vs_4_0, Overlay_VS() ) );
		SetGeometryShader( NULL );
		SetPixelShader( CompileShader( ps_4_0, Overlay_PS() ) );
	}
}
