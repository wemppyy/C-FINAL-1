using System;
using C__FINAL_1.app;

namespace C__FINAL_1
{
    class Program
    {
        static void Main(string[] args)
        {
            dictionary dict = new dictionary("English", "Spanish", "Dict1");
            ui ui = new ui(dict);

            ui.PrintDict(0);
        }
    }
}