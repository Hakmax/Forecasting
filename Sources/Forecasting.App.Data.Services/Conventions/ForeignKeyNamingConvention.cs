using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting.App.Data.Services.Conventions
{

    /*internal class Rewr :Convention
    {
        public Rewr() 
        {
            this.Properties<int>().Having(x => { x.Name.Contains("Id")}).Configure((x, y) =>
            {

            });
        )
        
    }*/

    internal class ForeignKeyNamingConvention : System.Data.Entity.ModelConfiguration.Conventions.IStoreModelConvention<AssociationType>
    {
        public void Apply(AssociationType item, DbModel model)
        {
 //           item.ann
            if (item.IsForeignKey)
            {
                var constraint = item.Constraint;
                
                //NormalizeForeignKeyProperties(constraint.ToProperties);
            }
        }

        private void NormalizeForeignKeyProperties(ReadOnlyMetadataCollection<EdmProperty> properties)
        {
            foreach (var property in properties)
            {
                var defaultPropertyName = property.Name;
                property.Name += "1";
                var ichUnderscore = defaultPropertyName.IndexOf('_');
                if (ichUnderscore <= 0) continue;
                /*if (defaultPropertyName.Length == 0)
                    continue;*/
                var navigationPropertyName = defaultPropertyName.Substring(0, ichUnderscore);
                var targetKey = defaultPropertyName.Substring(ichUnderscore + 1);

                string newPropertyName;
                if (targetKey.StartsWith(navigationPropertyName))
                    newPropertyName = targetKey;
                else
                    newPropertyName = navigationPropertyName + targetKey;

                property.Name = newPropertyName;
            }
        }

    }
}
