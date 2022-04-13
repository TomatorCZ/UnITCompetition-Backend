using ParserLib.XMLParser;
using ParserLib;

class Program
{
    static void Main(string[] args)
    {
        Parser parser = new Parser();
        var parsedFiles = parser.ParseFiles(args[1]);
    }

    //public static Options ParseOptions(string[] args) 
    //{
    //    if (args.Length != 5)
    //    {
    //        throw new ArgumentException();
    //    }

    //    if (args[1] != "DB")
    //    {
    //        throw new ArgumentException();
    //    }

    //    if (args[3] != "XML")
    //    {

    //        throw new ArgumentException();
    //    }


    //    return new Options() {
    //        Save = SaveEnum.DB,
    //        ConnectionString = args[2],
    //        Source = SourceEnum.XML,
    //        SourcePath = args[4]
    //    };
    //}

    //public static void ShowUsage() 
    //{
        
    //}

    //public enum SaveEnum { DB };
    //public enum SourceEnum { XML };
    //public class Options 
    //{
    //    public SaveEnum Save { get; set; }
    //    public string ConnectionString { get; set; }
    //    public SourceEnum Source { get; set; }
    //    public string SourcePath { get; set; }
    //}
}