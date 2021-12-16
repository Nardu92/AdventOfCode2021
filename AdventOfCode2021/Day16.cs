using System.Text;

public class Day16
{
    public static string ReadInputFile(string filename)
    {
        using var file = new System.IO.StreamReader(filename);
        return file.ReadLine()!;
    }

    public static string ConvertToBinary(string input)
    {
        var result = "";
        foreach (var c in input)
        {
            var d = Convert.ToInt32(c.ToString(), 16);
            result += Convert.ToString(d, 2).PadLeft(4, '0');
        }
        return result;
    }

    public static long GetVersions(string packetHex){
        var transmission = ConvertToBinary(packetHex);
        var packet = new Packet(transmission);

        return GetVersions(packet);
    }

    public static long GetVersions(Packet p){
        return p.Version + p.SubPackets.Select(x => GetVersions(x)).Sum();
    }
    public static long Sol1()
    {
        var input = ReadInputFile("Inputs\\input16.txt");
        return GetVersions(input);
    }
    public static long Sol2()
    {
        var input = ReadInputFile("Inputs\\input16.txt");
        return 0;
    }
}

public class Packet
{

    public string Transmission { get; private set; }

    public long Version { get; private set; }

    public int TypeId { get; private set; }

    public bool IsLiteralType { get; private set; }

    public long LiteralValue { get; private set; }

    public List<Packet> SubPackets { get; private set; }

    public bool LengthTypeID { get; private set; }

    public int NumberOfSubPackets { get; private set; }

    public Packet(string transmission)
    {
        Version = Convert.ToInt64(transmission[0..3], 2);
        TypeId = Convert.ToInt32(transmission[3..6], 2);
        IsLiteralType = TypeId == 4;
        SubPackets = new List<Packet>();
        if (IsLiteralType)
        {
            SetLiteralValue(transmission);
        }
        else
        {
            LengthTypeID = transmission[6] == '1';
            if (LengthTypeID)
            {
                NumberOfSubPackets = Convert.ToInt32(transmission[7..(7 + 11)], 2);
                SetSubPackets(transmission);
            }
            else
            {
                var length = 7 + 15 + Convert.ToInt32(transmission[7..(7 + 15)], 2);
                Transmission = transmission[..length];
                SetSubPackets(transmission);
            }
        }
    }

    private void SetLiteralValue(string transmission)
    {
        var stringBuilder = new StringBuilder();
        int i = 6;
        var last = false;
        while (!last)
        {
            if (transmission[i] == '0')
            {
                last = true;
            }
            stringBuilder.Append(transmission[(i + 1)..(i + 5)]);
            i += 5;
        }
        LiteralValue = Convert.ToInt64(stringBuilder.ToString(), 2);
        Transmission = transmission[..i];
    }

    public void SetSubPackets(string transmission)
    {
        if (!IsLiteralType)
        {
            if (!LengthTypeID)
            {
                var subTransmission = transmission[(6 + 15 + 1)..(Transmission.Length)];
                while (!string.IsNullOrEmpty(subTransmission))
                {
                    var packet = new Packet(subTransmission);
                    SubPackets.Add(packet);
                    subTransmission = subTransmission[packet.Transmission.Length..];
                }
            }else{
                var subTransmission = transmission[(6 + 11 + 1)..];
                for (int i = 0; i < NumberOfSubPackets; i++)
                {
                    var packet = new Packet(subTransmission);
                    SubPackets.Add(packet);
                    subTransmission = subTransmission[packet.Transmission.Length..];
                }
                var length = transmission.Length - subTransmission.Length;
                Transmission = transmission[..length];
            }
        }
    }

   
}
