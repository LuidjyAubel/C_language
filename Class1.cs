using System;
using System.Collections.Generic;
using System.Text;

namespace code
{
    class Bloc
    {
        Instruction instruction;
        Bloc suivant;
        Bloc()
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
    }
    class Instruction
    {
    }
    class Instruction_Let : Instruction
    {
        char variable;

        char variable2;        //soit var soit const si c'est une valeur ça contient un ! utiliser la valeur
        int valeur;
        bool param2var;
        Instruction_Let(char var,char val2, int val)
        {
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

