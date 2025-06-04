public class SmartPhone : Phone {

    public override void PowerOn() {

        Console.WriteLine("Smartphone is turned on.");

    }

    public override void Call(string number) {

        Console.WriteLine($"Calling {number}...");

    }

    public override int GetBatteryPercentage() {
        // Returning 80 just for demonstration purposes.
        return 80;
    }
}