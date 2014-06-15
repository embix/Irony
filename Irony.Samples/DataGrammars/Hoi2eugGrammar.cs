using Irony.Parsing;

namespace Irony.Samples.DataGrammars
{
    /// <summary>
    /// This class defines the Grammar for the Hoi2 eug file format.
    /// </summary>
    [Language("Hoi2eug", "0.1.0", "Hoi2 savegame/scenario grammar")]
    public class Hoi2eugGrammar : Grammar
    {
        public Hoi2eugGrammar()
        {
            GrammarComments = "Try to parse Hearts of Iron 2 Savegames";

            // terminals
            var hstring = new StringLiteral("String", "\"");
            var name = new IdentifierTerminal("Name", "_-", "_-");
            // normal countries are 3 letter uppercase abrevs like SWE
            // but there is a country called --- WTF?!
            name.AllChars = name.AllChars + "-";
            var hnumber = new NumberLiteral("Number", NumberOptions.AllowSign);

            // non-terminals
            var scenario = new NonTerminal("Scenario");
            var properties = new NonTerminal("Properties");
            var property = new NonTerminal("Property");
            var propertyName = new NonTerminal("PropertyName");
            var propertyValue = new NonTerminal("PropertyValue");
            var hvalue = new NonTerminal("Value");
            var hobject = new NonTerminal("Object");
            var harray = new NonTerminal("Array");
            var names = new NonTerminal("Names");
            var integers = new NonTerminal("Integers");
            
            scenario.Rule = properties;
            properties.Rule =  MakePlusRule(properties, property);
            property.Rule = propertyName + "=" + propertyValue;
            propertyName.Rule = name | hnumber;
            propertyValue.Rule = hvalue;
            hvalue.Rule = name | hnumber | hstring | hobject | harray;
            hobject.Rule = "{" + properties + "}";
            harray.Rule = "{" + names + "}" | "{" + integers + "}";
            names.Rule = MakePlusRule(names, name);
            integers.Rule = MakeStarRule(integers, hnumber);// we should have an integer and a float type...

            this.Root = scenario;
            MarkPunctuation("{", "}", "=");
        }
    }
}
