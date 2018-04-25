//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication9.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    
    public partial class Recipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recipe()
        {
            this.Ingrediants = new HashSet<Ingrediant>();
        }
    
        public int RecipeId { get; set; }
        public string Cuisine { get; set; }
        public string Name { get; set; }
        public string PreparationTime { get; set; }
        public string CookingTime { get; set; }
        public string Serves { get; set; }
        public string Method { get; set; }
        public string ImagePath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingrediant> Ingrediants { get; set; }

        public static int Count { get; set; }
        
        public void CreateIngrediants (int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Ingrediants.Add(new Ingrediant());
            }
        }
    }
}
