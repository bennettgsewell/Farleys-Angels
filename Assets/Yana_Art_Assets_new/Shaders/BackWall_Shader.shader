// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.05 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.05;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1759,x:32839,y:32701,varname:node_1759,prsc:2|emission-5206-RGB;n:type:ShaderForge.SFN_Tex2d,id:5206,x:32600,y:32721,ptovrint:False,ptlb:node_5206,ptin:_node_5206,varname:node_5206,prsc:2,tex:f44af501a8a50f34f965eeb7cdf4c068,ntxv:2,isnm:False|UVIN-3647-OUT,MIP-442-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2500,x:31497,y:32846,ptovrint:False,ptlb:Atlas_X,ptin:_Atlas_X,varname:node_2500,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:3351,x:31497,y:32961,ptovrint:False,ptlb:Atlas_Y,ptin:_Atlas_Y,varname:node_3351,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:8153,x:32034,y:32665,varname:node_8153,prsc:2|A-3497-OUT,B-2570-OUT;n:type:ShaderForge.SFN_Vector1,id:2570,x:31834,y:32744,varname:node_2570,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1336,x:31911,y:32905,varname:node_1336,prsc:2|A-3540-OUT,B-8042-OUT;n:type:ShaderForge.SFN_Append,id:3540,x:31715,y:32891,varname:node_3540,prsc:2|A-2500-OUT,B-3351-OUT;n:type:ShaderForge.SFN_Vector1,id:8042,x:31715,y:33053,varname:node_8042,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:3647,x:32370,y:32721,varname:node_3647,prsc:2|A-8153-OUT,B-1410-OUT;n:type:ShaderForge.SFN_Multiply,id:3497,x:32069,y:32487,varname:node_3497,prsc:2|A-7987-OUT,B-2130-OUT;n:type:ShaderForge.SFN_Vector1,id:7987,x:31763,y:32425,varname:node_7987,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Add,id:1410,x:32137,y:32982,varname:node_1410,prsc:2|A-1336-OUT,B-279-OUT;n:type:ShaderForge.SFN_Vector1,id:279,x:31910,y:33109,varname:node_279,prsc:2,v1:0.025;n:type:ShaderForge.SFN_Frac,id:2130,x:31773,y:32551,varname:node_2130,prsc:2|IN-9877-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7250,x:31129,y:32626,varname:node_7250,prsc:2;n:type:ShaderForge.SFN_Append,id:8548,x:31364,y:32611,varname:node_8548,prsc:2|A-7250-X,B-7250-Y;n:type:ShaderForge.SFN_Vector1,id:442,x:32370,y:32870,varname:node_442,prsc:2,v1:0;n:type:ShaderForge.SFN_Divide,id:9877,x:31560,y:32551,varname:node_9877,prsc:2|A-8548-OUT,B-6733-OUT;n:type:ShaderForge.SFN_Vector1,id:6733,x:31355,y:32483,varname:node_6733,prsc:2,v1:8;proporder:5206-2500-3351;pass:END;sub:END;*/

Shader "Custom/BackWall_Shader" {
    Properties {
        _node_5206 ("node_5206", 2D) = "black" {}
        _Atlas_X ("Atlas_X", Float ) = 0
        _Atlas_Y ("Atlas_Y", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            uniform sampler2D _node_5206; uniform float4 _node_5206_ST;
            uniform float _Atlas_X;
            uniform float _Atlas_Y;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float2 node_8548 = float2(i.posWorld.r,i.posWorld.g);
                float2 node_3647 = (((0.9*frac((node_8548/8.0)))*0.5)+((float2(_Atlas_X,_Atlas_Y)*0.5)+0.025));
                float4 _node_5206_var = tex2Dlod(_node_5206,float4(TRANSFORM_TEX(node_3647, _node_5206),0.0,0.0));
                float3 emissive = _node_5206_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
