using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BaboGame_test_2
{
    static class VectorOps
    {
        const float pi = (float)Math.PI;
        const float radToDegFact = (float)(0.5 * 360 / Math.PI);
        public static float Vector2ToRad(Vector2 vector)
        {
            float angle;
            float tan;
            if (vector.Y == 0)
            {
                angle = 0;
            }
            else
            {
                if (vector.X != 0)
                {
                    tan = vector.Y / vector.X;
                    angle = (float)Math.Atan(tan);
                }
                else angle = 0;
            }

            if(vector.X < 0)
            {
                angle = angle + pi;
            }

            return (float)angle;
        }


        public static float Vector2ToDeg(Vector2 vector)
        {
            float angle;
            float tan;
            if (vector.Y == 0)
            {
                angle = 0;
            }
            else
            {
                if (vector.X != 0)
                {
                    tan = Math.Abs(vector.Y / vector.X);
                    angle = (float)Math.Atan(tan);
                }
                else angle = 0;
            }

            angle *= radToDegFact;

            if(angle < 0)
            {
                angle += 360;
            }

            if (vector.X < 0 && vector.Y < 0)
            {
                angle += 180;
            }

            else if (vector.X > 0 && vector.Y < 0)
            {
                angle = 360 - angle;
            }

            else if (vector.X < 0 && vector.Y > 0)
            {
                angle = 180 - angle;
            }

            

            return angle;
        }
    }
}
