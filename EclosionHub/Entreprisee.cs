//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EclosionHub
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entreprisee
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Proprietaire { get; set; }
        public string Secteur_ { get; set; }
        public string Description { get; set; }
    
        public virtual Incube Incube { get; set; }
    }
}
