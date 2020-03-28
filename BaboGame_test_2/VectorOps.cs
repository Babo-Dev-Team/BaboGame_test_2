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
            // Calculate angle in 1st quadrant
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
                else
                {
                    if (vector.Y > 0)
                    {
                        angle = (0.5f * pi);
                    }
                    else angle = (1.5f * pi);
                }
            }

            if(vector.X < 0)
            {
                angle = angle + pi;
            }

            // Calculate real angle (all quadrants)
            if (vector.X < 0 && vector.Y < 0)
            {
                angle += pi;
            }
            else if (vector.X > 0 && vector.Y < 0)
            {
                angle = 2 * pi - angle;
            }
            else if (vector.X < 0 && vector.Y > 0)
            {
                angle = pi - angle;
            }
            return angle;
        }


        public static float Vector2ToDeg(Vector2 vector)
        {
            // Calculate angle in 1st quadrant
            float angle;
            float tan;
            if (vector.Y == 0)
            {
                if (vector.X > 0)
                    angle = 0;
                else
                    angle = pi;
            }
            else
            {
                if (vector.X != 0)
                {
                    tan = Math.Abs(vector.Y / vector.X); 
                    angle = (float)Math.Atan(tan);
                }
                else
                {
                    if (vector.Y > 0)
                    {
                        angle = (0.5f * pi);
                    }
                    else angle = (1.5f * pi);
                }
            }
            angle *= radToDegFact;
            if (angle < 0)
            {
                angle += 360;
            }

            // Calculate real angle (all quadrants)
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

        public static Vector2 UnitVector(Vector2 vector)
        {
            Vector2 unitVector;
            float vectorNorm = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            unitVector = vector / vectorNorm;
            return unitVector;
        }

        public static float ModuloVector (Vector2 vector)
        {
            float Modulo = (float)Math.Sqrt(Math.Pow(vector.X,2) + Math.Pow(vector.Y,2));
            return Modulo;
        }
    }
}
