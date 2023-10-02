namespace _02_1__SharedData
{
    [Serializable]
    public class Car
    {
        //public int MyProperty { get; set; }
        public string number { get; set; }
        public string run {get; set; }
        public string model { get; set; }
        public bool sank { get; set; }
        public bool painted { get; set; }
        public bool beaten { get; set; }
        public bool electro { get; set; }
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

 
}