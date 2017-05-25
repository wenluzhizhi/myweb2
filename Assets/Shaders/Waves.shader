Shader "Custom/Waves" {
	Properties{

         _Speed("Speed",Range(0,100))=2
          _SpeedV("Speed",Range(0,100))=2
	     _MainTex("MainTex",2D)="white"{}
	}

	Subshader{

	    Pass{

	    CGPROGRAM

	      
	       #pragma vertex vert
	       #pragma fragment frag

	       float4 _MainTex_ST;
	       sampler2D  _MainTex;
	       float _Speed;
	       float _SpeedV;
	       
	       struct a2v{

               float4 vertex:POSITION;
               float4 texcoord:TEXCOORD;

	       };

	       struct v2f{

	           float4 pos:SV_POSITION;
	           float2 texcoord:TEXCOORD0;

	       };

	       v2f vert(a2v v)
	       {

              v2f o;
              float waveValueA = sin(_Time.y + v.vertex.z )* _SpeedV;
              v.vertex.xyz = float3(v.vertex.x, v.vertex.y + waveValueA, v.vertex.z);  
              o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
              o.texcoord=v.texcoord.xy*_MainTex_ST.xy+_MainTex_ST.zw;
              return o;
	       }


	       fixed4 frag(v2f i):SV_Target{

	           i.texcoord.y+=sin(_Time.x*_Speed);
               float4 color=tex2D(_MainTex,i.texcoord);
               return color;
	       }

	    ENDCG

	    }
	}
}
