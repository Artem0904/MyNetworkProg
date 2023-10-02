namespace _02_1__SharedData
{
    [Serializable]
    public class Car
    {
        public string number = string.Empty;
        public string run = string.Empty;
        public string model = string.Empty;
        public bool painted = false;
        public bool beaten = false;
        public bool sank = false;
        public bool electro = false;
        public Car(string number, string run, string model, bool painted, bool beaten, bool sank, bool electro)
        {
            this.number = number;
            this.run = run;
            this.model = model;
            this.painted = painted;
            this.beaten = beaten;
            this.sank = sank;
            this.electro = electro;
        }
    }

    [Serializable]
    public class CarInfo
    {
        public string Info = string.Empty;
    }
}