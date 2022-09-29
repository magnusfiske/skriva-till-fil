// See https://aka.ms/new-console-template for more information



/*
 * De olika inputs som vi vill skicka in i objekten (hemsidorna)
 */
string[] techniques = { "   C#", "daTAbaser", "WebbuTVeCkling ", "clean Code   " };
string[] messagesToClass = { "Glöm inte att övning ger färdighet!", "Öppna boken på sida 257." };

// Vi skapar ett objekt för att kunna hantera en hemsida
WebsiteGenerator website = new WebsiteGenerator("Klass A", messagesToClass, techniques);

// Vi skapar en hemsida som tillåter styling, vi skickar in en färg utöver andra delar
StyledWebsiteGenerator styledWebsite = new StyledWebsiteGenerator("Klass A", "blue", messagesToClass, techniques);

// Vi skriver ut våra hemsidor först vanliga och sedan stylade
website.printPage();
Console.WriteLine("-----------------------");
styledWebsite.PrintToFile();


/*
 * Vi skapar ett interface (ett "kontrakt") med de delar som vår klass måste innehålla
 */
interface Website
{
    void printPage();
}

/*
 * Vi skapar vår WebsiteGenerator klass, med denna kan vi skapa objekt senare
 * Klassen innehåller data och behavior 
 */
class WebsiteGenerator : Website
{

    /*
     * De olika egenskaperna (datat) i varje objekt
     */
    string[] messagesToClass, techniques;
    string className;
    string kurser = "";

    /*
     * En konstruktor som tillåter oss att lägga in egen data i objektens egenskaper
     */
    public WebsiteGenerator(string className, string[] messageToClass, string[] techniques)
    {
        this.className = className;
        this.messagesToClass = messageToClass;
        this.techniques = techniques;
    }

    /*
     * Flera olika metoder för att utföra diverse funktionalitet
     * virtual = tillåter oss att override:a (göra egen version utav) metoden i ärvda klasser
     */
    virtual protected string printStart()
    {
        string start = "<!DOCTYPE html>\n<html>\n<body>\n<main>\n";
        return start;
    }
    string printWelcome(string className, string[] message)
    {
        string welcome = $"<h1> Välkomna {className}! </h1>";

        string welcomeMessage = "";

        foreach (string msg in message)
        {
            welcomeMessage += $"\n<p><b> Meddelande: </b> {msg} </p>";
        }

        return welcome + welcomeMessage;
    }
    string printKurser()
    {
        string kurser = courseGenerator(this.techniques);
        return kurser;
    }
    string printEnd()
    {
        string end = "</main>\n</body>\n</html>";
        return end;
    }

    public void printPage()
    {
        Console.WriteLine(printStart());
        Console.WriteLine(printWelcome(this.className, this.messagesToClass));
        Console.WriteLine(printKurser());
        Console.WriteLine(printEnd());
    }

    public void PrintToFile()
    {
        FileInfo file = new FileInfo(@"/Users/magnus/Desktop/Huset/minfil.txt");

        FileStream fs = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(printStart());
        sw.WriteLine(printWelcome(this.className, this.messagesToClass));
        sw.WriteLine(printKurser());
        sw.WriteLine(printEnd());

        sw.Close();
    }

    /*
     * En utility metod
     */
    string courseGenerator(string[] techniques)
    {

        foreach (string technique in techniques)
        {
            string tmp = technique.Trim();
            kurser += "<p>" + tmp[0].ToString().ToUpper() + tmp.Substring(1).ToLower() + "</p>\n";
        }

        return kurser;
    }
}

/*
 * Här ärver vi egenskaper och metoder ifrån WebsiteGenerator för att kunna återanvända delar i vår StyledWebsiteGenerator
 */
class StyledWebsiteGenerator : WebsiteGenerator
{
    // En extra egenskap
    string color;

    /*
     * En utökad konstruktor.
     * Vi vill lägga in alla del egenskaper som behövs i base-klassen vi ärvde ifrån
     * Och också lägga in en färg (data) i vår nya egenskap
     */
    public StyledWebsiteGenerator(string className, string color, string[] messageToClass, string[] techniques) : base(className, messageToClass, techniques)
    {
        this.color = color;
    }

    /*
     * Vi skapar en egen version av printStart (override:ar den) för att kunna få resultatet vi önskar
     */
    override protected string printStart()
    {
        return $"<!DOCTYPE html>\n<html>\n<head>\n<styles>\np {{ color: {this.color}; }}\n" +
                          "</styles>\n</head>\n<body>\n<main>\n";
    }
}