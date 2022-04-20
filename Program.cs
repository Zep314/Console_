

using System;

namespace Console_
{
        class vec2
    {
        public float x;
        public float y;
        public vec2(float value)
        {
            this.x=value;
            this.y=value;
        }
        public vec2(float _x, float _y)
        {
            this.x=_x;
            this.y=_y;
        }
        public void Print()
        {
            Console.Write($"[{x:f3};{y:f3}]");
        }
        public float Length() 
        { 
            return MathF.Sqrt(this.x * this.x + this.y * this.y); 
        }

        public static vec2 operator +( vec2 A, vec2 B) => new vec2(A.x+B.x,A.y+B.y);
        public static vec2 operator -( vec2 A, vec2 B) => new vec2(A.x-B.x,A.y-B.y);
        public static vec2 operator -( vec2 A, float B) => new vec2(A.x-B,A.y-B);
        public static vec2 operator *( vec2 A, vec2 B) => new vec2(A.x*B.x,A.y*B.y);
        public static vec2 operator *( vec2 A, float B) => new vec2(A.x*B,A.y*B);
        public static vec2 operator /( vec2 A, vec2 B) => new vec2(A.x/B.x,A.y/B.y);
    };
    class vec3
    {
        public float x;
        public float y;
        public float z;
        public vec3(float value)
        {
            this.x=value;
            this.y=value;
            this.z=value;
        }
        public vec3(float _x, vec2 _v)
        {
            this.x=_x;
            this.y=_v.x;
            this.z=_v.y;
        }
        public vec3(float _x, float _y, float _z)
        {
            this.x=_x;
            this.y=_y;
            this.z=_z;
        }
        public void Print()
        {
            Console.Write($"[{x:f3};{y:f3};{z:f3}]");
        }     
        public float Length() 
        { 
            return MathF.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z); 
        }
        public vec3 Norm()
        {
            return this / this.Length(); 
        }
        public vec3 Abs() 
        { 
            return new vec3(MathF.Abs(this.x), MathF.Abs(this.y), MathF.Abs(this.z)); 
        }
        public vec3 Sign() 
        { 
            return new vec3(MathF.Sign(this.x), MathF.Sign(this.y), MathF.Sign(this.z));
        }

        public void RotateX(float angle)
        {
            vec3 b = new vec3(0);
            b.z = this.z * MathF.Cos(angle) - this.y * MathF.Sin(angle);
            b.y = this.z * MathF.Sin(angle) + this.y * MathF.Cos(angle);
            this.z=b.z;
            this.y=b.y;
        }
        public void RotateY(float angle)
        {
            vec3 b = new vec3(0);
            b.x = this.x * MathF.Cos(angle) - this.z * MathF.Sin(angle);
            b.z = this.x * MathF.Sin(angle) + this.z * MathF.Cos(angle);
            this.x=b.x;
            this.z=b.z;
        }
        public void RotateZ(float angle)
        {
            vec3 b = new vec3(0);
            b.x = this.x * MathF.Cos(angle) - this.y * MathF.Sin(angle);
            b.y = this.x * MathF.Sin(angle) + this.y * MathF.Cos(angle);
            this.x=b.x;
            this.y=b.y;
        }
        public static vec3 operator +( vec3 A, vec3 B) => new vec3(A.x+B.x,A.y+B.y,A.z+B.z);
        public static vec3 operator -( vec3 A, vec3 B) => new vec3(A.x-B.x,A.y-B.y,A.z-B.z);
        public static vec3 operator *( vec3 A, vec3 B) => new vec3(A.x*B.x,A.y*B.y,A.z*B.z);
        public static vec3 operator *( vec3 A, float B) => new vec3(A.x*B,A.y*B,A.z*B);
        public static vec3 operator /( vec3 A, vec3 B) => new vec3(A.x/B.x,A.y/B.y,A.z/B.z);
        public static vec3 operator /( vec3 A, float B) => new vec3(A.x/B,A.y/B,A.z/B);
        public static vec3 operator /( float A, vec3 B) => new vec3(A/B.x,A/B.y,A/B.z);
        public static vec3 operator -(vec3 A) => new vec3(-A.x,-A.y,-A.z);
    }
    class Program
    {
        static void Main(string[] args)
        {
            float clamp(float value, float min, float max) { return MathF.Max(MathF.Min(value, max), min); }
            float dot(vec3 a, vec3 b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
            float Step(float edge, float x) { if (x > edge) {return 1;} else {return 0;} }
            vec3 reflect(vec3 rd, vec3 n) 
            {
                vec3 ret = new vec3(0);
                ret = rd - n * (2 * dot(n, rd));
                return ret;
            }
            vec3 Step1(vec3 edge, vec3 v) 
            {
                return new vec3(Step(edge.x, v.x), Step(edge.y, v.y), Step(edge.z, v.z)); 
            }

            vec2 Sphere(vec3 ro, vec3 rd, float r) 
            {
                float b = dot(ro, rd);
                float c = dot(ro, ro) - r * r;
                float h = b * b - c;
                if (h < 0.0) return new vec2((float)-1.0);
                h = MathF.Sqrt(h);
                return new vec2 (-b - h, -b + h);
            }
            vec2 box(vec3 ro, vec3 rd, vec3 boxSize, vec3 outNormal) 
            {
                vec3 m = new vec3(1.0f) / rd;
                vec3 n = new(0);
                n = m * ro;
                vec3 k = new vec3(0);
                k = m.Abs() * boxSize;  ////////////!!!!!!!!!!!!!!!!!!!!!!!!!!
                vec3 t1 = new vec3(0);
                t1 = -n - k;
                vec3 t2 = new vec3(0);
                t2 = -n + k;
                float tN = MathF.Max(MathF.Max(t1.x, t1.y), t1.z);
                float tF = MathF.Min(MathF.Min(t2.x, t2.y), t2.z);
                if ((tN > tF) || (tF < 0.0)) return new vec2(-1.0f);
                vec3 yzx = new vec3(t1.y, t1.z, t1.x);
                vec3 zxy = new vec3(t1.z, t1.x, t1.y);
                outNormal = -rd.Sign() * Step1(yzx, t1) * Step1(zxy, t1);
                return new vec2(tN, tF);
            }
            float plane(vec3 ro, vec3 rd, vec3 p, float w)
            {
                return -(dot(ro, p) + w) / dot(rd, p);
            }


///////////////////////////////// ПРОГРАММА //////////////////////
            int width = Console.WindowWidth;
 	        int height = Console.WindowHeight;
            float aspect = (float) width / height;
            float pixelAspect = 11.0f / 24.0f;
            char[] gradient = {' ','.',':','!','/','r','(','l','1','Z','4','H','9','W','8','$','@'};//" .:!/r(l1Z4H9W8$@";
            int gradientSize = gradient.Length - 1;
            byte [] screen = new byte[width*height];

            for (int t = 0; t < 10000; t++)
            {
                vec3 light = new vec3(-0.5f, 0.5f, -1.0f).Norm();
                vec3 spherePos = new vec3(0, 3, 0);
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                    {
                        vec2 uv = new vec2(i,j) / new vec2(width, height) * 2.0f - 1.0f;
                        uv.x *= aspect * pixelAspect;
                        //uv.x += (float)MathF.Sin(t * 0.01f);
                        vec3 ro = new vec3(-6,0,0);
                        vec3 rd = new vec3(2.0f,uv).Norm();

                        ro.RotateY(0.25f);
                        rd.RotateY(0.25f);
                        ro.RotateZ(t*0.01f);
                        rd.RotateZ(t*0.01f);

                        float diff = 1;
                        for (int k = 0; k < 3; k++)
                        {
                            float minIt = 99999;
                            vec2 intersection = Sphere(ro - spherePos,rd,1);
                            vec3 n = new vec3(0);
                            float albedo = 1;
                            if (intersection.x > 0 )
                            {
                                vec3 itPoint = new vec3(0);
                                itPoint = ro - spherePos + rd * intersection.x;
                                minIt = intersection.x;
                                n = itPoint.Norm();
                            } 
                            vec3 boxN = new vec3(0);
                            vec3 one = new vec3(1);
    			            intersection = box(ro, rd, one, boxN);
                            if (intersection.x > 0 && intersection.x < minIt)
                            {
                                minIt = intersection.x;
                                n = boxN;
                            }

                            vec3 v_3 = new vec3(0,0,-1);
                            intersection = new vec2(plane(ro, rd, v_3, 1f));
    			            if (intersection.x > 0 && intersection.x < minIt)
                            {
	    		                minIt = intersection.x;
						        n = new vec3(0, 0, -1);
    						    albedo = 0.1f;
		    	            }

                            if (minIt < 99999)
                            {
                                diff *= ( dot(n,light) * 0.5f + 0.5f ) * albedo;
                                ro = ro + rd * (minIt - 0.01f);
                                rd = reflect(rd,n);
                            }
                            else break;
                        }
                        int color = (int)(diff * 20);
                        color = (int)clamp(color,0,gradientSize);
                        char pixel=gradient[color]; 
                        screen[i+j*width]=(byte)pixel;
                    }
                var stdout = Console.OpenStandardOutput(width * height);
                stdout.Write(screen,0,screen.Length);
//                System.Threading.Thread.Sleep(2);
            }
        }
    }
}
