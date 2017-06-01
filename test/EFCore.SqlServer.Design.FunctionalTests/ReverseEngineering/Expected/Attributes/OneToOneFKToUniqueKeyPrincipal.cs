using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E2ETest.Namespace.SubDir
{
    [Table("OneToOneFKToUniqueKeyPrincipal")]
    public partial class OneToOneFktoUniqueKeyPrincipal
    {
        [Column("OneToOneFKToUniqueKeyPrincipalID1")]
        public int OneToOneFktoUniqueKeyPrincipalId1 { get; set; }
        [Column("OneToOneFKToUniqueKeyPrincipalID2")]
        public int OneToOneFktoUniqueKeyPrincipalId2 { get; set; }
        [Required]
        [StringLength(20)]
        public string SomePrincipalColumn { get; set; }
        [Column("OneToOneFKToUniqueKeyPrincipalUniqueKey1")]
        public int OneToOneFktoUniqueKeyPrincipalUniqueKey1 { get; set; }
        [Column("OneToOneFKToUniqueKeyPrincipalUniqueKey2")]
        public int OneToOneFktoUniqueKeyPrincipalUniqueKey2 { get; set; }

        public OneToOneFktoUniqueKeyDependent OneToOneFktoUniqueKeyDependent { get; set; }
    }
}
