Shader "Unlit/Panoramic360" {
//	Properties {
//		_Color ("Main Color", Color) = (1,1,1,1)
//		_MainTex ("Base (RGB)", 2D) = "white" {}
//		_BumpMap ("Bump (RGB) Illumin (A)", 2D) = "bump" {}
//	}
//	SubShader {
//		UsePass "Self-Illumin/VertexLit/BASE"
//		UsePass "Bumped Diffuse/PPL"
//	} 
//	FallBack "Diffuse"
//}

Properties
   {
       _MainTex ("Base (RGB)", 2D) = "white" {}
       _Color ("Main Color", Color) = (1,1,1,0.5)
   }
   SubShader 
   {
      Tags { "RenderType" = "Opaque" }
      //This is used to print the texture inside of the sphere
      Cull Front
      CGPROGRAM
      #pragma surface surf SimpleLambert
      half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
      {
         half4 c;
         c.rgb = s.Albedo;
         return c;
      }
      
      sampler2D _MainTex;
      struct Input
      {
         float2 uv_MainTex;
         float4 myColor : COLOR;
      };
 
      fixed3 _Color;
      void surf (Input IN, inout SurfaceOutput o)
      {
         //This is used to mirror the image correctly when printing it inside of the sphere
         IN.uv_MainTex.x = 1 - IN.uv_MainTex.x;
         fixed3 result = tex2D(_MainTex, IN.uv_MainTex)*_Color;
         o.Albedo = result.rgb;
         o.Alpha = 1;
      }
      ENDCG
   }
   Fallback "Diffuse"
}

//source: https://medium.com/@verochan/how-to-make-a-360º-image-viewer-with-unity3d-b1aa9f99cabb#.kosrs9nyk
