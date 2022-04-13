using ParserLib.XMLParser;
using ParserLib;
using Shared.Models;

class Program
{
    static void Main(string[] args)
    {
        //parsing
        Parser parser = new Parser();
        var parsedFiles = parser.ParseFiles(args[1]);
        
        //save
        using (var ctx = new CommonDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<CommonDbContext>(), @"DataSource = C:\Users\husak\Hackathons\InITCompetition\UnITCompetition-Backend\BackendWebAPI\UnIT.db;"))
        {
            int id = 0;
            int idGroup = 0;
            foreach (var item in parsedFiles.Take(10))
            {
                Console.WriteLine(id);
                AddNewHead(item.Item1, ctx).Wait();
                foreach (var itemG in item.Item2)
                {
                    itemG.Item1.HeadId = id;
                    AddNewGroup(itemG.Item1, ctx).Wait();
                    foreach (var itemT in itemG.Item2)
                    {
                        itemT.GroupId = idGroup;
                        AddNewTest(itemT, ctx);
                    }
                    idGroup++;
                }
                id++;
            }
        }
    }

    public static async Task<int> AddNewHead(Head head, CommonDbContext ctx)
    {
        var result = ctx.Heads.Add(head).Entity.Id;
        await ctx.SaveChangesAsync();
        return result;
    }

    public static async Task<int> AddNewTest(Test test, CommonDbContext ctx)
    {
        var result = ctx.Tests.Add(test).Entity.Id;
        await ctx.SaveChangesAsync();
        return result;
    }

    public static async Task<int> AddNewGroup(Group group, CommonDbContext ctx)
    {
        var result = ctx.Groups.Add(group).Entity.Id;
        await ctx.SaveChangesAsync();

        return result;
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