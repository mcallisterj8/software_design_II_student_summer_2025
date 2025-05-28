public class Bird : Animal, IFlyable, IComparable {

    public void Fly() {
        Console.WriteLine("The bird is flying.");
    }

    public void Speak(){
        Console.WriteLine("Tweet!");
    }

    public void Land() {
        throw new NotImplementedException();
    }

    public int CompareTo(object? obj) {
        throw new NotImplementedException();
    }
}