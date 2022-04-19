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
            return MathF.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);; 
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
            this.z = this.z * MathF.Cos(angle) - this.y * MathF.Sin(angle);
            this.y = this.z * MathF.Sin(angle) + this.y * MathF.Cos(angle);
        }
        public void RotateY(float angle)
        {
            this.x = this.x * MathF.Cos(angle) - this.z * MathF.Sin(angle);
            this.z = this.x * MathF.Sin(angle) + this.z * MathF.Cos(angle);
        }
        public void RotateZ(float angle)
        {
            this.x = this.x * MathF.Cos(angle) - this.y * MathF.Sin(angle);
            this.y = this.x * MathF.Sin(angle) + this.y * MathF.Cos(angle);
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
            vec2 Sphere(vec3 ro, vec3 rd, float r) 
            {
                float b = dot(ro, rd);
                float c = dot(ro, ro) - r * r;
                float h = b * b - c;
                if (h < 0.0) return new vec2((float)-1.0);
                h = MathF.Sqrt(h);
                return new vec2 (-b - h, -b + h);
            }


///////////////////////////////// ПРОГРАММА //////////////////////
            int width = Console.WindowWidth;
 	        int height = Console.WindowHeight;
            float aspect = (float) width / height;
            float pixelAspect = 11.0f / 24.0f;
            char[] gradient = {' ','.',':','!','/','r','(','l','1','Z','4','H','9','W','8','$','@'};//" .:!/r(l1Z4H9W8$@";
            int gradientSize = gradient.Length - 2;

            byte [] screen = new byte[width*height+1];
//            Console.WriteLine($"{height}");
            for (int t = 0; t < 1000; t++)
            {
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                    {
                        vec2 uv = new vec2(i,j) / new vec2(width, height) * 2.0f - 1.0f;
//                        float x = (float)i / width * 2.0f - 1.0f;
//                        float y = (float)j / height * 2.0f - 1.0f;
                        uv.x *= aspect * pixelAspect;
                        uv.x += (float)MathF.Sin(t * 0.01f);
                        vec3 ro = new vec3(-5,0,0);
                        vec3 rd = new vec3(1,uv).Norm();

                        char pixel = ' ';
                        float dist = uv.Length(); //MathF.Sqrt(uv.x * uv.x + uv.y * uv.y);
//                        int color = (int)(1.0f / dist);  Видео 10:10
                        int color = 1;

                        color = (int)clamp(color,0,gradientSize);
                        pixel=gradient[color]; 
                        screen[i+j*width]=(byte)pixel;
                    }
                var stdout = Console.OpenStandardOutput(width * height);
                stdout.Write(screen,0,screen.Length);
                System.Threading.Thread.Sleep(3);
            }
        }
    }
}
