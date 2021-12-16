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

    public static long Sol1()
    {
        var input = ReadInputFile("Inputs\\input16e.txt");
        return 0;
    }
    public static long Sol2()
    {
        var input = ReadInputFile("Inputs\\input16e.txt");
        return 0;
    }
}

public class Packet{

    public string Transmission { get; set; }

    public int Version { get; set; }
    public int TypeId { get; set; }

    public bool IsLiteralType {get; set;} 

    public long LiteralValue {get; private set;}

    private List<Packet> _subPackets {get; set;}

    public bool LengthTypeID {get; set;}

    public int TotalLengthInBits {get; set;}

    public int NumberOfSubPackets {get; set;}

    public int TransmissionLength {get; set;}
    public Packet(string transmission){
        Version = Convert.ToInt32(transmission[0..3], 2);
        TypeId = Convert.ToInt32(transmission[3..6], 2);
        IsLiteralType = TypeId == 4;
        _subPackets = new List<Packet>();
        if(IsLiteralType){
            SetLiteralValue(transmission);
            Transmission = transmission;
        }else{
            LengthTypeID = transmission[6] == '1';
            if(LengthTypeID){
                NumberOfSubPackets = Convert.ToInt32(transmission[7..(7+11)], 2);
            }else{
                TotalLengthInBits = Convert.ToInt32(transmission[7..(7+15)], 2);
                TransmissionLength = 7 + 15 + TotalLengthInBits;
                Transmission = transmission;
            }
        }
    }

    private void SetLiteralValue(string transmission){
        var stringBuilder = new StringBuilder();
        int i = 6;
        var last = false;
        while(!last){
            if (transmission[i] == '0'){
                last = true;
            }
            stringBuilder.Append(transmission[(i+1)..(i+5)]);
            i += 5;
        }
        TransmissionLength = i;
        LiteralValue = Convert.ToInt64(stringBuilder.ToString(), 2);
    }

    public List<Packet> GetSubPackets(){
        if(!IsLiteralType){
            if(!_subPackets.Any()){
                if (!LengthTypeID){
                    var subTransmission = Transmission[(6+15)..(TransmissionLength-1)];
                    var firstPacket = new Packet(subTransmission);
                    var f = firstPacket.Transmission.Length;
                }
                //_subPackets.Add();

            }
        }
        return _subPackets;
    }
}
