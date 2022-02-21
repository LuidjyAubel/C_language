using System;
using System.IO;
using System.Globalization;

namespace code
{
    class Program               //Classe bibliotheque (classe abstracte)
    {
        public static void Main()
        {

            //Class2.Leprogramme = new Bloc();
            Class2.LesVariables = new Variables();
            Class2.Compiler("C:/Users/AUBElui/Documents/text.txt");
            Class2.Leprogramme.afficher();
            //Class2.Leprogramme.afficher();
            Class2.Leprogramme.executer();


            Console.WriteLine(""); Console.WriteLine("FIN Normale."); Console.ReadLine();
        }
    }
}

    
    

