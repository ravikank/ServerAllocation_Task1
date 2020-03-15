using System.Collections.Generic;
using System.Linq;

namespace ServerAllocation.Service
{
    public class Allocator
    {
        private readonly IList<Server> _servers;

        public Allocator(IList<Server> servers)
        {
            _servers = servers;
        }

        public Server Allocate()
        {
            int selectedServerId = -1;
            int currentWeight = 0;
            while (true)
            {
                selectedServerId = (selectedServerId + 1) % _servers.Count();
                if (selectedServerId == 0)
                {
                    currentWeight = currentWeight - GetGreatestCommonDivisorOfWeight();
                }

                if (currentWeight <= 0)
                {
                    currentWeight = _servers.Max(s => s.Weight);
                    if (currentWeight == 0)
                        return null;
                }

                Server selectedServer = _servers.Where(s => s.Id == selectedServerId).FirstOrDefault();

                if (selectedServer == null)
                    return null;

                if (!_servers.Any(s => s.Threshold > s.CPUUsage))
                {
                    return null;
                }

                if (selectedServer.Weight >= currentWeight && selectedServer.CPUUsage < selectedServer.Threshold)
                    return selectedServer;
            }
        }

        private int GetGreatestCommonDivisorOfWeight()
        {
            if (_servers == null || _servers.Count <= 0)
                return 0;

            int result = _servers.First().Weight;

            for (int i = 1; i < _servers.Count; i++)
            {
                result = CalculateGreatestCommonDivisor(result, _servers[i].Weight);
            }

            return result;
        }

        private int CalculateGreatestCommonDivisor(int number1, int number2)
        {
            if (number2 == 0)
            {
                return number1;
            }
            return CalculateGreatestCommonDivisor(number2, number1 % number2);
        }
    }
}
