namespace ServerAllocation.Service
{
    public class Server
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Threshold { get; set; }
        public int CPUUsage { get; set; }

        public bool Validate()
        {
            if (Weight <= 0 || Weight > 100)
            {
                return false;
            }

            if (Threshold <= 0 || Threshold > 100)
            {
                return false;
            }
            return true;
        }

        public void UpdateUsage(int cpuUsage)
        {
            CPUUsage = cpuUsage;
        }
    }
}
