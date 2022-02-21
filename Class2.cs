﻿using System;
using System.IO;
using System.Globalization;

namespace code
{
    abstract class Class2
    {
         static public Bloc Leprogramme; // le programme à construire
        static public Bloc LeBlocEnCourant; //auxilière de constructeur
        static public StreamReader fichierentre;
         public static Variables LesVariables;
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
        static Bloc Lirebloc()
        {
            Bloc Ancienbloc = LeBlocEnCourant;
            LeBlocEnCourant = new Bloc();
            int i;
            string ligne = lireligne();
            i = 0;
            string token1 = ExtraireToken(ref i, ligne);
            if (token1 != "{") Erreur("{ manquante");
            // lire jusqua l'acolade fermante
             ligne = lireligne();
            i = 0;
             token1 = ExtraireToken(ref i, ligne);
            while (token1 != "}")
            {
                TraiterInstruction(ligne);
                ligne = lireligne();
                i = 0;
                token1 = ExtraireToken(ref i, ligne);
            }
            Bloc nouveaubloc = LeBlocEnCourant;     //BIDOUILLE
            LeBlocEnCourant = Ancienbloc;           //RESTAURE LE BLOC EN COURS
            return nouveaubloc;
        }
        static void TraiterInstruction(string ligne)
        {
            int i = 0; //FAUT LA METTRE ICI, PAS EN GLOBAL
            cpt++;     //A mettre au début afin de ne pas afficher "Ligne 0 : "
                       //Console.Write("Ligne N°" + cpt + " :");
            string token = ExtraireToken(ref i, ligne);
            switch (token)
            {

                case "LET": traiterLet(i, ligne); break;
                case "ADD": traiterAdd(i, ligne); break;
                case "SUB": traiterSub(i, ligne); break;
                case "MUL": traiterMul(i, ligne); break;
                case "DIV": traiterDiv(i, ligne); break;
		case "IF": traiterIF(i, ligne); break;
		case "WHILE": traiterWhile(i, ligne); break;
                case "MOD": traiterMod(i, ligne); break;
                case "WRITE": traiterWrite(i, ligne); break;
                case "": break;

                default: Console.WriteLine("ERROR: Instruction inconnue ! <" + token+">"); break;
            }
            while (token != "")
            {
                //Console.Write(" " + token + " ");
                token = ExtraireToken(ref i, ligne);

            }
        }
        static string lireligne()
        {
            return fichierentre.ReadLine();
        }
        public static void Compiler(string chemin)
        {
            // COMPILER LE FICHIER
            Leprogramme = new Bloc();    //prog vide !
            //String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                 fichierentre = new StreamReader(chemin);
                 Leprogramme = Lirebloc();
                    /*
                //Read the first line of text
                line = fichierentre.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //on fait traiter la ligne
                    traiterLigne(line);

                    //Read the next line
                    line = fichierentre.ReadLine();
                }*/
                //close the file
                fichierentre.Close();
                    
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
            Console.WriteLine("Erreur :" + a);
            return -1;
        }
        static bool estVariable(string token)
        {
            if (token.Length != 1) return false;
            return ((token[0] >= 'A') && (token[0] <= 'Z'));
        }
        static bool estchifre(char token)
        {
            return (token >= '0' && token <= '9');
        }
        static bool estNombre(string token)
        {
            if (token.Length == 0) return false;
            for (int i = 0; i < token.Length; i++)
            {
                if (!estchifre(token[i])) return false;
            }
            return true;
        }
        static bool estComparateur(string token)
        {
            switch (token)
            {
                case "=": return true;
                case "!=": return true;
                case "<": return true;
                case ">": return true;
                case "<=": return true;
                case ">=": return true;
                default: return false;
            }
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
            if (!estNombre(param2)) Erreur("Param2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
            if (reste != "") Erreur("Let n'accepte que 2 parametre");
            Instruction_Let instruction = new Instruction_Let(param1[0], Int32.Parse(param2));
            LeBlocEnCourant.ajouter(instruction);
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
        static int traiterWrite(int i, string ligne)
        {
            string param1 = ExtraireToken(ref i, ligne);
            string reste = ExtraireToken(ref i, ligne);

            if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
            if (reste != "") Erreur("ADD n'accepte que 3 parametre");
            Instruction_Write instruction = new Instruction_Write(param1[0]);
            LeBlocEnCourant.ajouter(instruction);
            return -1;
        }
        static int traiterAdd(int i, string ligne)
        {
            string param1 = ExtraireToken(ref i, ligne);
            string param2 = ExtraireToken(ref i, ligne);
            string param3 = ExtraireToken(ref i, ligne);
            string reste = ExtraireToken(ref i, ligne);

            if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
            if (!estVariable(param2)) Erreur("PARAM2 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
            if (!estVariable(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
            if (reste != "") Erreur("ADD n'accepte que 3 parametre");
            Instruction_ADD instruction = new Instruction_ADD(param1[0], param2[0], param3[0]);
            LeBlocEnCourant.ajouter(instruction);
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
        static int traiterIF(int i, string ligne)
        {
            string param1 = ExtraireToken(ref i, ligne);
            string param2 = ExtraireToken(ref i, ligne);
            string param3 = ExtraireToken(ref i, ligne);
            string reste = ExtraireToken(ref i, ligne);

            if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
            if (!estComparateur(param2)) Erreur("PARAM2 DOIT ETRE UN COMPARATEUR");
            if (!estVariable(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
            if (reste != "") Erreur("IF n'accepte que 3 parametre");
            Bloc blocif = Lirebloc();
            Instruction_IF instruction = new Instruction_IF(param1[0],param2,param3[0], blocif);
            LeBlocEnCourant.ajouter(instruction);
            return -1;
        }
        static int traiterWhile(int i, string ligne)
        {
            string param1 = ExtraireToken(ref i, ligne);
            string param2 = ExtraireToken(ref i, ligne);
            string param3 = ExtraireToken(ref i, ligne);
            string reste = ExtraireToken(ref i, ligne);

            if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
            if (!estComparateur(param2)) Erreur("PARAM2 DOIT ETRE UN COMPARATEUR");
            if (!estVariable(param3)) Erreur("PARAM3 DOIT ETRE UNE VARIABLE OU UNE CONSTANTE");
            if (reste != "") Erreur("While n'accepte que 3 parametre");
            Bloc blocif = Lirebloc();
            Instruction_IF instruction = new Instruction_IF(param1[0], param2, param3[0], blocif);
            LeBlocEnCourant.ajouter(instruction);
            return -1;
        }
        static int traiterINC(int i, string ligne)
        {
            string param1 = ExtraireToken(ref i, ligne);
            string reste = ExtraireToken(ref i, ligne);

            if (!estVariable(param1)) Erreur("PARAM1 DOIT ETRE UNE VARIABLE");
            if (reste != "") Erreur("ADD n'accepte que 3 parametre");
            Instruction_INC instruction = new Instruction_INC(param1[0]);
            LeBlocEnCourant.ajouter(instruction);
            return -1;
        }
        static int cpt = 0;

        static void traiterLigne(string ligne)
        {
            int i = 0; //FAUT LA METTRE ICI, PAS EN GLOBAL
            cpt++;     //A mettre au début afin de ne pas afficher "Ligne 0 : "
                       //Console.Write("Ligne N°" + cpt + " :");
            string token = ExtraireToken(ref i, ligne);
            switch (token)
            {

                case "LET": traiterLet(i, ligne); break;
                case "ADD": traiterAdd(i, ligne); break;
                case "SUB": traiterSub(i, ligne); break;
                case "IF": traiterIF(i, ligne); break;
                case "WHILE": traiterWhile(i, ligne); break;
                case "MUL": traiterMul(i, ligne); break;
                case "DIV": traiterDiv(i, ligne); break;
                case "MOD": traiterMod(i, ligne); break;
                case "WRITE": traiterWrite(i, ligne); break;
                case "INC": traiterINC(i, ligne); break;
                case "//": break;
                case "":break;

                default: Console.WriteLine("ERROR: Instruction inconnue ! <" + token+">"); break;
            }
            while (token != "")
            {
                //Console.Write(" " + token + " ");
                token = ExtraireToken(ref i, ligne);

            }
            //Console.WriteLine("");

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
    }
}