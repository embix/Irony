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

            // terminals
            var hstring = new StringLiteral("String", "\"");
            var hnumber = new NumberLiteral("Number");
            var name = new IdentifierTerminal("Name");

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

            scenario.Rule = properties;
            properties.Rule =  MakePlusRule(properties, property);
            property.Rule = propertyName + "=" + propertyValue;
            propertyName.Rule = name;
            propertyValue.Rule = hvalue;
            hvalue.Rule = name | hnumber | hstring | hobject | harray;
            hobject.Rule = "{" + properties + "}";
            harray.Rule = "{" + names + "}";
            names.Rule = MakePlusRule(names, name);

            this.Root = scenario;
            MarkPunctuation("{", "}", "=");
        }
    }
}
