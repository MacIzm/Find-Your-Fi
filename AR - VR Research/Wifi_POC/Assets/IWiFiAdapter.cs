
#if !UNTY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public interface IWiFiAdapter<T>
    {
        uint GetSignal(string ssid);
        double getRSSI();
        string GetNetworkReport();
        List<T> getNetworks();
    }
}
#endif