public abstract class Phone : ElectronicDevice {
    
    public abstract void Call(string number);

    public override void ShowManufacturer() {
        Console.WriteLine("Manufacturer: PhoneTech Co.");
    }

}