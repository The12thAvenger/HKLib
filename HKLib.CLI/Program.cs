using HKLib.Serialization.hk2018.Binary;
using HKLib.Serialization.hk2018.Xml;

namespace HKLib.CLI;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine(
                "This application converts between binary hkx tagfiles and xml tagfiles. Drag and drop a file onto the exe to use.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        HavokBinarySerializer binarySerializer = new();
        HavokXmlSerializer xmlSerializer = new();

        string path = args.First(x => !x.EndsWith(".compendium"));
        if (path.EndsWith(".xml"))
        {
            string outputPath = path[..^3] + "hkx";
            Backup(outputPath);

            try
            {
                binarySerializer.Write(xmlSerializer.Read(path), outputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("File Conversion failed.");
                Console.WriteLine(e);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
        else if (path.EndsWith(".hkx"))
        {
            string outputPath = path[..^3] + "xml";
            Backup(outputPath);

            if (args.FirstOrDefault(x => x.EndsWith(".compendium")) is { } compendiumPath)
            {
                binarySerializer.LoadCompendium(compendiumPath);
            }

            try
            {
                xmlSerializer.Write(binarySerializer.Read(path), outputPath);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    $"The file \"{path}\" contains a compendium reference. Drag and drop the associated compendium reference onto the exe along with the file to convert it.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("File Conversion failed.");
                Console.WriteLine(e);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Unsupported file format. Please supply an xml or hkx file.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Creates a backup of the file at the given path if it exists to avoid it being overwritten by appending ".bak" to its
    /// name.
    /// </summary>
    public static void Backup(string path)
    {
        if (!File.Exists(path)) return;
        File.Move(path, path + ".bak", true);
    }
}