using System;
using System.IO;
using System.Globalization;

namespace code
{
    class Program
    {
        class Test
        {

            static string ExtraireToken(ref int indice, string ligne)
            {
                string token = "";
                int lgn = ligne.Length;

                if (ligne == "") return token;
                if (lgn <= indice) return token;


                while (ligne[indice] <= ' ')
                {
                    indice++;
                    if (indice >= lgn) return token;
                }

                while (ligne[indice] > ' ')
                {
                    token += ligne[indice];
                    indice++;
                    if (indice >= lgn) return token;
                }

                return token;
            }

            static void traiterFichier(string chemin)
            {
                String line;
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(chemin);
                    //Read the first line of text
                    line = sr.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        //on fait traiter la ligne
                        traiterLigne(line);

                        //Read the next line
                        line = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                }
                catch (Exception f)
                {
                    Console.WriteLine("Exception: " + f.Message);
                }
                finally
                {
                    Console.WriteLine("");
                    Console.WriteLine("Fin du traitement.");
                }
            }
            static int Erreur(string a)
            {
                Console.WriteLine("Erreur :"+ a);
                return -1;
            }
            static bool estVariable(string token)
            {
                if (token.Length != 1) return false;
                return ((token[0]>= 'A') && (token[0] <= 'Z'));
            }
            static bool estchifre(char token )
            {
                return (token >= '0' && token <= '9');
            }
            static bool estConstant(string token)
            {
                //if (token.Length != 1) return false;
                for (int i = 0; i < token.Length; i++)
                    if (estchifre(token[i])) return true;
                   return false;

            }
            static bool estVarConst(string token)
            {
                return estVariable(token) || estConstant(token);
            }
            static int traiterLet(int i, string ligne)
            {
                //le resultat INT ne sert à rien c'est just pour sortir rapidement
                string param1 = ExtraireToken(ref i, ligne);
                string param2 = ExtraireToken(ref i, ligne);
                string reste = ExtraireToken(ref i, ligne);
                if (!estVariable(param1)) Erreur("Param1 DOIT ETRE UNE VARIABLE");
                if (!estVarConst(param2)) Erreur("Param2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (reste != "") Erreur("Let n'accepte que 2 parametre");
                return -1;
            }
            static int traiterSub(int i, string ligne)
            {
                string param1 = ExtraireToken(ref i, ligne);
                string param2 = ExtraireToken(ref i, ligne);
                string param3 = ExtraireToken(ref i, ligne);
                string reste = ExtraireToken(ref i, ligne);
                if (!estVariable(param1)) Erreur("Param1 DOIT ETRE UNE VARIABLE");
                if (!estVarConst(param2)) Erreur("PARAM2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (!estVarConst(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (reste != "") Erreur("ADD n'accepte que 3 parametre");
                return -1;
            }
            static int traiterAdd(int i, string ligne)
            {
                string param1 = ExtraireToken(ref i, ligne);
                string param2 = ExtraireToken(ref i, ligne);
                string param3 = ExtraireToken(ref i, ligne);
                string reste = ExtraireToken(ref i, ligne);

                if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
                if (!estVarConst(param2)) Erreur("PARAM2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (!estVarConst(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (reste != "") Erreur("SUB n'accepte que 3 parametre");
                return -1;
            }
            static int traiterMul(int i, string ligne)
            {
                string param1 = ExtraireToken(ref i, ligne);
                string param2 = ExtraireToken(ref i, ligne);
                string param3 = ExtraireToken(ref i, ligne);
                string reste = ExtraireToken(ref i, ligne);

                if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
                if (!estVarConst(param2)) Erreur("PARAM2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (!estVarConst(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (reste != "") Erreur("SUB n'accepte que 3 parametre");
                return -1;
            }
            static int traiterDiv(int i, string ligne)
            {
                string param1 = ExtraireToken(ref i, ligne);
                string param2 = ExtraireToken(ref i, ligne);
                string param3 = ExtraireToken(ref i, ligne);
                string reste = ExtraireToken(ref i, ligne);

                if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
                if (!estVarConst(param2)) Erreur("PARAM2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (!estVarConst(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (reste != "") Erreur("DIV n'accepte que 3 parametre");
                return -1;
            }
            static int traiterMod(int i, string ligne)
            {
                string param1 = ExtraireToken(ref i, ligne);
                string param2 = ExtraireToken(ref i, ligne);
                string param3 = ExtraireToken(ref i, ligne);
                string reste = ExtraireToken(ref i, ligne);

                if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
                if (!estVarConst(param2)) Erreur("PARAM2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (!estVarConst(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
                if (reste != "") Erreur("MOD n'accepte que 3 parametre");
                return -1;
            }
            static int cpt = 0;

            static void traiterLigne(string ligne)
            {
                int i = 0; //FAUT LA METTRE ICI, PAS EN GLOBAL
                cpt++;     //A mettre au début afin de ne pas afficher "Ligne 0 : "
                Console.Write("Ligne N°" + cpt + " :");
                string token = ExtraireToken(ref i, ligne);
                switch (token)
                {
                    case "LET": traiterLet(i, ligne); break;
                    case "ADD": traiterAdd(i,ligne) ; break;
                    case "SUB": traiterSub(i, ligne); break;
                    case "MUL": traiterMul(i, ligne); break;
                    case "DIV": traiterDiv(i, ligne); break;
                    case "MOD": traiterMod(i, ligne); break;
                    case "": break;

                    default: Console.WriteLine("ERROR: Instruction inconnue !" + token);break;
                }
                while (token != "")
                {
                    Console.Write("<" + token + ">");
                    token = ExtraireToken(ref i, ligne);
                    
                }
                Console.WriteLine("");

            }
            static void ecrire(string chemin)
            {
                try
                {
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter(chemin);
                    //Write a line of text
                    sw.WriteLine("Hello World!!");
                    //Write a second line of text
                    sw.WriteLine("From the StreamWriter class");
                    //Close the file
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                }
            }
            public static void Main()
            {
                
                //traiterFichier("C:\\Users\\AUBElui\\text.txt");

                Variables A = new Variables();
                A.setVariable('A', 12);
                A.setVariable('D', 9);
                A.setVariable('W', 2);
                A.setVariable('Z', 23);
                A.Dump();
                A.getVariable('D');
                A.getVariable('Z');
                A.getVariable('A');
                A.getVariable('W');

                Console.WriteLine(""); Console.WriteLine("FIN Normale."); Console.ReadLine();
            }
        }
    }
        }
    

