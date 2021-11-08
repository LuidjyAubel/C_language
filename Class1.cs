using System;
using System.Collections.Generic;
using System.Text;

namespace code
{
    class Bloc
    {
        Instruction instruction;
        Bloc suivant;
        public Bloc()
        {
            suivant = null;
        }
        public void ajouter(Instruction instruction)
        {
            if (this.suivant == null)
            {
                this.suivant = new Bloc();
                this.instruction = instruction;
            }
            else this.suivant.ajouter(instruction);
        }
        public void afficher()
        {
            if (this.suivant != null)
            {
                Console.WriteLine(instruction.name);
                suivant.afficher();         //récursif
            }
        }
        public void executer()
        {
            if (this.suivant != null)
            {
                instruction.execute();
                suivant.executer();         //récursif
            }
        }
    }
    class Instruction
    {
        public string name;
    }
    class Instruction_Let : Instruction
    {
        char variable;
        int valeur;
        //char variable2;        //soit var soit const si c'est une valeur ça contient un ! utiliser la valeur
        //bool param2var;
       public Instruction_Let(char var, int val)
        {
            this.name = "LET "+var+" "+val;
            //this.name = "" + var + " = " + val+";";  traduction C#
            //this.name = "$" + var + " = " + val+";";
            this.variable = var;
            this.valeur = val;
        }
  /*      public void execute()
        {
            int lavaleur;
            if (param2var)
            {
                lavaleur = recuperervaleur(variable2);
            }
            else lavaleur = valeur;
            rangervaleurdansvariable(lavaleur, variable);
            //ranger la valeur dans la variable
        }*/
    }
    class Instruction_ADD : Instruction
    {
        char variable;
        char variable2;
        char variable3;
        //char variable2;        //soit var soit const si c'est une valeur ça contient un ! utiliser la valeur
        //bool param2var;
        public Instruction_ADD(char var, char var2, char var3)
        {
            this.name = "ADD " + var + " " + var2 +" "+var3;
            //this.name = "" + var + " = " + var2+" + "+var3+";";  //traduction C#
            //this.name = "$" + var + " = $" + var2+" $"+var3+;";
            this.variable = var;
            this.variable2 = var2;
            this.variable3 = var3;
        }
        /*      public void execute()
              {
                  int lavaleur;
                  if (param2var)
                  {
                      lavaleur = recuperervaleur(variable2);
                  }
                  else lavaleur = valeur;
                  rangervaleurdansvariable(lavaleur, variable);
                  //ranger la valeur dans la variable
              }*/
    }
    class Instruction_Write : Instruction
    {
        char variable;
        //char variable2;        //soit var soit const si c'est une valeur ça contient un ! utiliser la valeur
        //bool param2var;
        public Instruction_Write(char var)
        {
            this.name = "WRITE " + var;
            //this.name = "" + var + " = " + val+";";  traduction C#
            //this.name = "$" + var + " = " + val+";";
            this.variable = var;
        }
        /*      public void execute()
              {
                  int lavaleur;
                  if (param2var)
                  {
                      lavaleur = recuperervaleur(variable2);
                  }
                  else lavaleur = valeur;
                  rangervaleurdansvariable(lavaleur, variable);
                  //ranger la valeur dans la variable
              }*/
    }
    class Variables
    {
        protected int[] tabvar;
        public Variables()
        {
            tabvar = new int[26];
            Init();
        }
        public void Init()
        {
            for (int i = 0; i < 26; i++)
            {
                tabvar[i] = 0;
            }
        }
        public void Dump()
        {
            for (int i = 0; i < 26; i++)
            {
                Console.Write(" " + tabvar[i] + " ");
            }

        }
        public void setVariable(char nomVar, int val)
        {
            for (int i = 0; i < 26; i++)
            {
                //i = charname - 'A';
                if (i == nomVar - 'A')
                {
                    tabvar[i] = val;
                    return;
                }
            }
        }
        public int getVariable(char nomVar)
        {
            for (int i = 0; i < 26; i++)
            {
                if (i == nomVar - 'A')
                {
                    Console.WriteLine();
                    Console.WriteLine(nomVar + " = " + tabvar[i]);
                }
            }
            return 1;
        }
    }
}

