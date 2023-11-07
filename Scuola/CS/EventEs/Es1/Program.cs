using System.Formats.Asn1;
using System.Security.AccessControl;

// Used to modfy the obj when an event happens
public class CShapeEvengArgs : EventArgs
{
    public double NewArea {get; set;}

    public CShapeEvengArgs(double Area)
    {
        NewArea = Area;
    }
}

// create the abstract method Draw, and the method to invoke the event that derived classes can override (virtual)
public abstract class CShape
{
    protected double Area {get; set;}

    public event EventHandler<CShapeEvengArgs> ShapeChanged;

    public abstract void Draw();

    public virtual void OnShapeChanged(CShapeEvengArgs e)
    {
        ShapeChanged?.Invoke(this, e);
    }
}


public class CCircle : CShape
{
    public double Radius {get; set;}

    public CCircle(double Radius)
    {
        this.Radius = Radius;
        Area = Radius * Radius * 3.14;
    }
    public override void Draw()
    {
        double thickness = 0.4;
        ConsoleColor BorderColor = ConsoleColor.Yellow;
        Console.ForegroundColor = BorderColor;
        char symbol = '*';

        Console.WriteLine();
        double rIn = this.Radius- thickness, rOut = this.Radius + thickness;

        for (double y = this.Radius; y >= -this.Radius; --y)
        {
            for (double x = -this.Radius; x < rOut; x += 0.5)
            {
                double value = x * x + y * y;

                if (value >= rIn * rIn && value <= rOut * rOut)
                    Console.Write(symbol);
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    public void Update(double Radius)
    {
        Area = Radius * Radius * 3.14;
        OnShapeChanged(new CShapeEvengArgs(Area));
    }

    public virtual void OnShapeChanged(CShapeEvengArgs e)
    {
        base.OnShapeChanged(e);
    }


}

public class CRectangle : CShape
{
    public double Length {get; set;}

    public double Width {get; set;}

    public CRectangle(double Length, double Width)
    {
        this.Length = Length;
        this.Width = Width;
        Area = this.Length * this.Width;
    }
    public override void Draw()
    {
        for (int i = 0; i < (int)Width; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < (int)Length; j++)
            {
                Console.Write("*");
            }
        }   
    }

    public void Update(double Length, double Width)
    {
        this.Length = Length;
        this.Width = Width;
        Area = Length * Width;
        base.OnShapeChanged(new CShapeEvengArgs(Area));
    }

}

public class CShapeContainer
{
    public readonly List<CShape> shapeList;

    public CShapeContainer()
    {
        shapeList = new List<CShape>();
    }

    public void AddShape(CShape Shape)
    {
        shapeList.Add(Shape);

        Shape.ShapeChanged += HandleShapeChanged;
    }

    public void HandleShapeChanged(object sender, CShapeEvengArgs e)
    {
        if (sender is CShape shape)
        {
            shape.Draw();
        }
    }
}

class Test
    {
        static void Main()
        {
            //Create the event publishers and subscriber
            var circle = new CCircle(10);
            var rectangle = new CRectangle(12, 9);
            var container = new CShapeContainer();

            // Add the shapes to the container.
            container.AddShape(circle);
            container.AddShape(rectangle);

            // Cause some events to be raised.
            circle.Update(8);
            rectangle.Update(20, 5);
        }
    }