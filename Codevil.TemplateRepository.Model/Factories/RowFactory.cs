using System.Collections;

namespace Codevil.TemplateRepository.Model.Factories
{
    public class RowFactory : Codevil.TemplateRepository.Factories.RowFactory
    {
        public override System.Collections.Hashtable MapPluralInflections()
        {
            Hashtable hashtable = base.MapPluralInflections();

            hashtable.Add("PERSON", "PEOPLE");

            return hashtable;
        }
    }
}
