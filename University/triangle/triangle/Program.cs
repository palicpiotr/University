using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triangle
{
    class Triangle
    {
        private double a;
        private double b;
        private double c;

        public Triangle(int storona1, int storona2, int storona3)
        {
            this.a = storona1;
            this.b = storona2;
            this.c = storona3;
        }

        public bool IsExistTriangle()
        {
            //bool flag = false;

            if ((a < b + c) && (b < a + c) && (c < a + b))
            {
              // flag = true;
                Console.WriteLine("The triangle exists");
                return true;
                
            }
            else
            {
               // flag = false;
                Console.WriteLine("The triangle doesn't exist");
                return false;
                
            }
           
        }

        public void MedianesOfTriangle()
        {
            try
            {
                double median;
                if (IsExistTriangle() == true)
                {
                    median = 1 / 2 * (Math.Sqrt(2 * (b * b) + 2 * (c * c) - (a * a)));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("The exception is: " + ex);
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle(1, 2, 3);
            triangle.IsExistTriangle();
            Console.WriteLine("The mediane of the triangle equal: ");
            triangle.MedianesOfTriangle();
            
            double median;
            double a = 1;
            double b = 2;
            double c = 3;
            median = (1 / 2 * (Math.Sqrt((2 * (b * b)) + (2 * (c * c)) - (a * a))));
            Console.WriteLine("The median of triangle is: " + median);
        }
    }
}
