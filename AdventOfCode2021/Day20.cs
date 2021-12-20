using System.Text;

public class Day20
{
    public static ImageEnhancer ReadInputFile(string filename, int offset = 4)
    {
        using var file = new System.IO.StreamReader(filename);
        var decoder = file.ReadLine()!;
        string line ;
        file.ReadLine();
        var lines = new List<string>();        
        while ((line = file.ReadLine()) != null)
        {
            lines.Add(line); 
        }

        return new ImageEnhancer(decoder,lines, offset);
    }

    public static long Sol1()
    {
        var imageEnhancer = ReadInputFile("Inputs\\input20.txt");
        imageEnhancer.Enhance();
        imageEnhancer.Enhance();
        
        return imageEnhancer.CountLitPixels();
    }
    public static long Sol2()
    {
        var imageEnhancer = ReadInputFile("Inputs\\input20.txt", 100);
        for (int i = 1; i < 51; i++)
        {
            imageEnhancer.Enhance(i % 2 == 0);
        }
        return imageEnhancer.CountLitPixels();
    }
}

public class ImageEnhancer{

    public bool[] Decoder { get; }

    public bool [,] Bitmap { get; private set;}

    private int offset;

    public ImageEnhancer(string decoder, List<string> bitmap, int offset)
    {
        this.offset = offset;
        Decoder = decoder.Select(x => x == '#').ToArray();
        Bitmap = new bool[bitmap.Count + offset*2, bitmap.First().Length + offset*2];
        for (int y = 0; y < bitmap.Count; y++)
        {
            for (int x = 0; x < bitmap.First().Length; x++)
            {
                Bitmap[y + offset, x + offset] = bitmap[y][x] == '#';
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < Bitmap.GetLength(0); i++)
        {
            for (int j = 0; j < Bitmap.GetLength(1); j++)
            {
                sb.Append(Bitmap[i, j] ? '#' : '.');
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
    public void Enhance(bool odd = false){
        var newBitmap = new bool[Bitmap.GetLength(0)+2, Bitmap.GetLength(1)+2];

        for (int y = 1; y < Bitmap.GetLength(0)-1; y++)
        {
            for (int x = 1; x < Bitmap.GetLength(1)-1; x++)
            {
                var value = GetPixelValue(x,y);

                newBitmap[y + 1, x + 1] = Decoder[value];
            }
        }
        // if(odd && Decoder[0]){
        //     for (int x = 0; x < Bitmap.GetLength(1); x++)
        //     {
        //         newBitmap[0, x] = true;
        //         newBitmap[newBitmap.GetLongLength(0) -1, x] = true;
        //     }
        //     for (int y = 0; y < newBitmap.GetLength(0); y++)
        //     {
        //         newBitmap[y, 0] = true;
        //         newBitmap[y, newBitmap.GetLongLength(1) -1] = true;
        //     }
        // }
        Bitmap = newBitmap;
    }

    public int GetPixelValue(int x, int y)
    {
        var sb = new StringBuilder();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                sb.Append(Bitmap[y + i, x + j] ? '1' : '0');
            }
        }
        return Convert.ToInt32(sb.ToString(), 2);
    }

    internal long CountLitPixels()
    {
        long litPixels = 0;
        for(int y= offset; y< Bitmap.GetLength(0)-offset;y++){
            for(int x= offset; x< Bitmap.GetLength(1)-offset;x++){
                if(Bitmap[y,x]){
                    litPixels++;
                }
            }
        }
        return litPixels;
    }

    public void WriteResultToFile(string filename)
    {
        using var file = new System.IO.StreamWriter(filename);
        file.WriteLine(ToString());
    }
}
