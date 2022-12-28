using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    internal class Element
    {
        public Coordinates coords;
        public Shape shape;
        public int size;
        public int color;

        public Element(Coordinates coords, Shape shape, int size, int color, int fill_color)
        {
            this.coords = coords;
            this.shape = shape;
            this.size = size;
            this.color = color;
        }

        public Element(Point point1, Shape shape, int size, int color, int fill_color)
        {
            this.coords = new Coordinates(point1);
            this.shape = shape;
            this.size = size;
            this.color = color;
        }

        public Element(Point point1, Point point2, Shape shape, int size, int color, int fill_color)
        {
            this.coords = new Coordinates(point1, point2);
            this.shape = shape;
            this.size = size;
            this.color = color;
        }
        public Element()
        {
            coords = new Coordinates(new Point(-1, -1));
        }
    }

    internal class Coordinates
    {
        public Point point1;
        public Point point2;

        public Coordinates(Point point1)
        {
            this.point1 = point1;
            this.point2 = new Point();
        }

        public Coordinates(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public Coordinates()
        {
            this.point1 = new Point();
            this.point2 = new Point();
        }
    }

    enum Shape
    {
        line = 0,
        ellipse = 1,
        rectangle = 2
    }
}
