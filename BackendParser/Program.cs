using ParserLib.XMLParser;
using Shared.Models;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //parsing
        Parser parser = new Parser();
        var parsedFiles = parser.ParseFiles(args[1]);

        Stopwatch sw = new Stopwatch();

        sw.Start();
        //save
        using (var ctx = new CommonDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CommonDbContext>(), @"DataSource = C:\Users\husak\Hackathons\InITCompetition\UnITCompetition-Backend\BackendWebAPI\UnIT.db;"))
        {
            foreach (var item in parsedFiles)
            {
                int id = AddNewHead(item.Item1, ctx).Result;
                Console.WriteLine(id);
                foreach (var itemG in item.Item2)
                {
                    itemG.Item1.HeadId = id;
                    int idGroup = AddNewGroup(itemG.Item1, ctx).Result;
                    foreach (var itemT in itemG.Item2)
                    {
                        itemT.GroupId = idGroup;
                        AddNewTest(itemT, ctx);
                    }
                }
            }
        }

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    public static async Task<int> AddNewHead(Head head, CommonDbContext ctx)
    {
        var a = ctx.Heads.Add(head);
        await ctx.SaveChangesAsync();
        return a.Entity.Id;
    }

    public static async Task<int> AddNewTest(Test test, CommonDbContext ctx)
    {
        var result = ctx.Tests.Add(test);
        await ctx.SaveChangesAsync();
        return result.Entity.Id;
    }

    public static async Task<int> AddNewGroup(Group group, CommonDbContext ctx)
    {
        var result = ctx.Groups.Add(group);
        await ctx.SaveChangesAsync();

        return result.Entity.Id;
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