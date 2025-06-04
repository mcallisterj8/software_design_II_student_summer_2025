public abstract class ElectronicDevice {

    public abstract void PowerOn();

    public abstract int GetBatteryPercentage();

    public virtual void ShowManufacturer() {
        Console.WriteLine("Manufacturer: Generic Electronics Inc.");
    }

}