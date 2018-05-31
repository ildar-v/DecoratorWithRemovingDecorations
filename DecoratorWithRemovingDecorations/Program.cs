using System;

namespace DecoratorWithRemovingDecorations
{
    class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza = new BulgarianPizza();
            pizza = new TomatoPizza(pizza);
            pizza = new CheesePizza(pizza);
            pizza = new TomatoPizza(pizza);
            Console.WriteLine($"Name: {pizza.Name}; Cost: { pizza.GetCost()}");

            pizza = pizza.DeleteDecoration(typeof(TomatoPizza));
            Console.WriteLine("After removing the ingredient TomatoPizza:");
            Console.WriteLine($"Name: {pizza.Name}; Cost: { pizza.GetCost()}");

            pizza = pizza.DeleteDecoration(typeof(TomatoPizza));
            Console.WriteLine("After removing the ingredient TomatoPizza:");
            Console.WriteLine($"Name: {pizza.Name}; Cost: { pizza.GetCost()}");

            pizza = pizza.DeleteDecoration(typeof(CheesePizza));
            Console.WriteLine("After removing the ingredient CheesePizza:");
            Console.WriteLine($"Name: {pizza.Name}; Cost: { pizza.GetCost()}");

            pizza = pizza.DeleteDecoration(typeof(CheesePizza));
            Console.WriteLine("After removing the ingredient CheesePizza:");
            Console.WriteLine($"Name: {pizza.Name}; Cost: { pizza.GetCost()}");

            Console.ReadLine();
        }

        abstract class Pizza
        {
            public Pizza(string n)
            {
                this.Name = n;
            }
            public Pizza PreviousPizza { get; protected set; }
            public virtual string Name { get; protected set; }
            public abstract int GetCost();

            public Pizza DeleteDecoration(Type type)
            {
                if (PreviousPizza != null)
                {
                    if (this.GetType() == type)
                    {
                        return PreviousPizza;
                    }
                    PreviousPizza = PreviousPizza.DeleteDecoration(type);
                }
                return this;
            }
        }

        class ItalianPizza : Pizza
        {
            public ItalianPizza() : base("Italian pizza")
            { }
            public override int GetCost()
            {
                return 10;
            }
        }
        class BulgarianPizza : Pizza
        {
            public BulgarianPizza()
                : base("Bulgarian pizza")
            { }
            public override int GetCost()
            {
                return 8;
            }
        }

        abstract class PizzaDecorator : Pizza
        {
            public PizzaDecorator(string n, Pizza pizza) : base(n)
            {
                this.PreviousPizza = pizza;
            }
        }

        class TomatoPizza : PizzaDecorator
        {
            public TomatoPizza(Pizza p)
                : base(p.Name, p)
            { }

            public override string Name { get => PreviousPizza.Name + ", with tomatoes"; }

            public override int GetCost()
            {
                //Console.Write("[Tomato]"); // To check the correctness of the algorithm.
                return PreviousPizza.GetCost() + 3;
            }
        }

        class CheesePizza : PizzaDecorator
        {
            public CheesePizza(Pizza p)
                : base(p.Name, p)
            { }

            public override string Name { get => PreviousPizza.Name + ", with cheese"; }

            public override int GetCost()
            {
                //Console.Write("[Сheese]"); // To check the correctness of the algorithm.
                return PreviousPizza.GetCost() + 5;
            }
        }
    }
}
